using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocaliseMeMeshUI : MonoBehaviour 
{
    #region Public Attributs
    public TextMeshProUGUI _TextMeshProUGUI;
    public string LocalisationKey = "";
    public bool UpperCase;
    #endregion

    #region Private Attributs
    #endregion

    void OnEnable()
    {
        ChangeLanguage();
        LocalisationManager.Instance.OnChangeLanguage += ChangeLanguage;
    }

    void OnDisable()
    {
        if (!GameManager.IsGameExitting)
            LocalisationManager.Instance.OnChangeLanguage -= ChangeLanguage;
    }

    private void ChangeLanguage()
    {
        if (LocalisationKey == "")
            LocalisationKey = name;

        string text = LocalisationManager.Instance.Loc(LocalisationKey);
        _TextMeshProUGUI.text = UpperCase ? text.ToUpper() : text;
    }

    public void ChangeKey(string newKey)
    {
        LocalisationKey = newKey;
        ChangeLanguage();
    }
}
