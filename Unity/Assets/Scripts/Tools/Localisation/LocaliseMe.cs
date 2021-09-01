using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class LocaliseMe : MonoBehaviour 
{
    #region Public Attributs
    public Text _Text;
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
        _Text.text = UpperCase ? text.ToUpper() : text;
    }

    public void Refresh()
    {
        ChangeLanguage();
    }
}
