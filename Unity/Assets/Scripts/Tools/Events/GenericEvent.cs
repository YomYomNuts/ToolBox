using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericEvent : MonoBehaviour
{
    #region Public Attributs
    public float timeBeforeToLaunch;
    public UnityEvent eventToLaunch;
    #endregion

    #region Protected Attributs
    #endregion

    #region Private Attributs
    #endregion

    #region Properties
    #endregion

    void Start()
    {
    }

    public void Launch()
    {
        if (enabled)
            StartCoroutine(LaunchEvent());
    }

    public void InstantLaunch()
    {
        if (enabled)
            eventToLaunch.Invoke();
    }

    IEnumerator LaunchEvent()
    {
        yield return new WaitForSeconds(timeBeforeToLaunch);
        eventToLaunch.Invoke();
    }
}
