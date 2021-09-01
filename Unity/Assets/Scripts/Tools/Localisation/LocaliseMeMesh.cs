using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class LocaliseMeMesh : MonoBehaviour 
{
    #region Public Attributs
    public TextMeshPro _TextMeshPro;
    public string LocalisationKey = "";
    public bool UpperCase;
    #endregion

    #region Private Attributs
    #endregion

    void Awake()
    {
        _TextMeshPro.enabled = false;
    }

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
        _TextMeshPro.text = UpperCase ? text.ToUpper() : text;
    }

    public void Refresh()
    {
        ChangeLanguage();
    }
}
