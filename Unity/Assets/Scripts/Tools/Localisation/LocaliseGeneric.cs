using UnityEngine;
using System.Collections;
using TMPro;

public class LocaliseGeneric : MonoBehaviour 
{
    #region Public Attributs
    public GameObject _FR;
    public GameObject _EN;
    public GameObject _DE;
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
        _FR.SetActive(false);
        _EN.SetActive(false);
        _DE.SetActive(false);
        string language = LocalisationManager.Instance.CurrentLanguage;
        if (language == "FR")
            _FR.SetActive(true);
        else if (language == "EN")
            _EN.SetActive(true);
        else if (language == "GE")
            _DE.SetActive(true);

    }

    public void Refresh()
    {
        ChangeLanguage();
    }
}
