using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LocSet = System.Collections.Generic.Dictionary<string, string>;


public enum LOCATION_LANGUAGE
{
    FR = 0,
    EN,
    GE,
    Length
}

public class LocalisationManager : MonoBehaviour
{
    #region SINGLETON
    static LocalisationManager instance;

    public static LocalisationManager Instance
    {
        get
        {
            if(instance == null)
                instance = GameObject.FindObjectOfType<LocalisationManager>();
            if(instance == null)
                instance = new GameObject("LocalisationManager").AddComponent<LocalisationManager>();
            return instance;
        }
    }
    #endregion

    #region Delegates
    public event Action OnChangeLanguage;
    #endregion

    #region Private Attributs
    private Dictionary<string, LocSet> _AllLocalisations = new Dictionary<string, LocSet>();
    private LocSet _CurrentLanguageLocalisations;
    private string _CurrentLanguage = "";
    #endregion

    #region Properties
    public string CurrentLanguage
    {
        get
        {
            return _CurrentLanguage;
        }
        set
        {
            if (value == _CurrentLanguage)
                return;

            LocSet newLanguageLocalisation;
            if (!_AllLocalisations.TryGetValue(value, out newLanguageLocalisation))
                Debug.LogError("There is no language called '" + value + "'");
            else
            {
                _CurrentLanguageLocalisations = newLanguageLocalisation;
                _CurrentLanguage = value;
                PlayerPrefs.SetString("Language", _CurrentLanguage);
            }

            if (OnChangeLanguage != null)
                OnChangeLanguage();
        }
    }

    public IEnumerable<string> AllLanguages
    {
        get
        {
            if (_AllLocalisations == null)
            {
                Debug.LogWarning("'_AllLocalisations' is null. This is impossible. You broke reality");
                yield break;
            }

            foreach (var l in _AllLocalisations.Keys)
                yield return l;
        }
    }

    public IEnumerable<KeyValuePair<string, string>> AllCurrentLanguageLocalisations
    {
        get
        {
            foreach (var kp in _CurrentLanguageLocalisations)
            {
                if(kp.Value == "")
                    Debug.LogError("No localisation for " + kp.Key);
                yield return kp;
            }
        }
    }
    #endregion

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        // Create Default Language Possible
        for (int i = 0; i < (int)LOCATION_LANGUAGE.Length; ++i)
        {
            _AllLocalisations.Add(((LOCATION_LANGUAGE)i).ToString(), new Dictionary<string, string>());
        }

        // Determinate the language
        string defaultLanguage = "EN";
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.French)
                defaultLanguage = "FR";
            else if (Application.systemLanguage == SystemLanguage.English)
                defaultLanguage = "EN";
            else if (Application.systemLanguage == SystemLanguage.German)
                defaultLanguage = "GE";
        }
        CurrentLanguage = PlayerPrefs.GetString("Language", defaultLanguage);

        LoadCSV();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            NextLanguage();
    }

    public void NextLanguage(int directionLanguage = 1)
    {
        int index = -1;
        int size = (int)LOCATION_LANGUAGE.Length;
        for (int i = 0; i < size; ++i)
        {
            string language = ((LOCATION_LANGUAGE)i).ToString();
            if (language == CurrentLanguage)
            {
                index = i;
                break;
            }
        }
        index = (((index + directionLanguage) % size) + size) % size;
        CurrentLanguage = ((LOCATION_LANGUAGE)index).ToString();
    }

    public string Loc(string key)
    {
        string loc;
        if (!_CurrentLanguageLocalisations.TryGetValue(key, out loc))
        {
            Debug.LogWarning("No localisation for key '" + key + "' exists");
            return "#?MISSING?" + key + "#";
        }
        else
            return loc.Trim().Replace("###", "\n");
    }

    public string LocFromPool(string key, string separator, short ID)
    {
        string[] locPool = LocPool(key, separator);
        return locPool[ID < (short)(locPool.Length - 1) ? ID : (short)0];
    }

    public string[] LocPool(string key, string separator)
    {
        string loc;
        if (!_CurrentLanguageLocalisations.TryGetValue(key, out loc))
        {
            Debug.LogWarning("No localisation for key '" + key + "' exists");
            return new string[1] { "#?MISSING?" + key + "#" };
        }
        else
        {
            string[] locSplit = loc.Replace("\n", String.Empty).Split(separator.ToCharArray()[0]);
            for (short iter = 0; iter < locSplit.Length - 1; iter++)
                locSplit[iter] = locSplit[iter].Trim();
            return locSplit;
        }
    }

    public void AddLoc(string parLanguage, string parKey, string parValue)
    {
        if (parLanguage == "FR" || parLanguage == "EN" || parLanguage == "GE")
            _AllLocalisations[parLanguage].Add(parKey, parValue);
    }

    static string SPLIT_RE = @";";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    private List<Dictionary<string, object>> Read(TextAsset parFile)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

        if (parFile == null)
            return list;
 
        var lines = Regex.Split(parFile.text, LINE_SPLIT_RE);
        if (lines.Length <= 1)
            return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "")
                continue;
 
            var entry = new Dictionary<string, object>();
            for (var j = 0 ; j < header.Length && j < values.Length; j++ )
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                    finalvalue = n;
                else if (float.TryParse(value, out f))
                    finalvalue = f;
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }

    private void LoadCSV()
    {
        List<Dictionary<string, object>> listDatasCSV = Read(GameManager.Instance._GameData.CSVGeneral);
        if (listDatasCSV.Count > 0)
        {
            List<string> keysCol = listDatasCSV[0].Keys.ToList();
            foreach (Dictionary<string, object> line in listDatasCSV)
            {
                string idLoc = (string)line[keysCol[0]];
                if (idLoc.Trim() != "")
                {
                    for (int i = 1; i < keysCol.Count; ++i)
                    {
                        if (keysCol[i].Trim() != "")
                            LocalisationManager.Instance.AddLoc(keysCol[i], idLoc, (string)line[keysCol[i]]);
                    }
                }
            }
        }

        /*listDatasCSV = Read(GameManager.Instance.GameData.CSVStory);
        if (listDatasCSV.Count > 0)
        {
            List<string> keysCol = listDatasCSV[0].Keys.ToList();
            foreach (Dictionary<string, object> line in listDatasCSV)
            {
                string idLoc = (string)line[keysCol[0]];
                if (idLoc.Trim() != "")
                {
                    for (int i = 1; i < keysCol.Count; ++i)
                    {
                        if (keysCol[i].Trim() != "")
                        {
                            LocalisationManager.Instance.AddLoc(keysCol[i], idLoc, (string)line[keysCol[i]]);
                        }
                    }
                }
            }
        }*/
    }
}
