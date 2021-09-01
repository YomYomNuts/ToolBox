using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    #region Public Attributs
    public LayerMask layermask = Physics.DefaultRaycastLayers;
    public float timeBeforeToLaunchTriggerEnter;
    public UnityEvent eventOnTriggerEnter;
    public float timeBeforeToLaunchTriggerStay;
    public UnityEvent eventOnTriggerStay;
    public float timeBeforeToLaunchTriggerExit;
    public UnityEvent eventOnTriggerExit;
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

    public virtual bool CanTrigger(Collider other)
    {
        return enabled && (layermask == (layermask | (1 << other.gameObject.layer)));
    }

    void OnTriggerEnter(Collider other)
    {
        if (CanTrigger(other))
            StartCoroutine(EventTrigger(timeBeforeToLaunchTriggerEnter, eventOnTriggerEnter));
    }
    public void EventOnTriggerEnter(Collider2D _other)
    {
        eventOnTriggerEnter.Invoke();
    }

    void OnTriggerStay(Collider other)
    {
        if (CanTrigger(other))
            StartCoroutine(EventTrigger(timeBeforeToLaunchTriggerStay, eventOnTriggerStay));
    }
    public void EventOnTriggerStay(Collider2D _other)
    {
        eventOnTriggerEnter.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        if (CanTrigger(other))
            StartCoroutine(EventTrigger(timeBeforeToLaunchTriggerExit, eventOnTriggerExit));
    }
    public void EventOnTriggerExit(Collider2D _other)
    {
        eventOnTriggerEnter.Invoke();
    }

    IEnumerator EventTrigger(float _timer, UnityEvent _event)
    {
        yield return new WaitForSeconds(_timer);
        _event.Invoke();
    }
}
