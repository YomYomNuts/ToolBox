using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger2D : MonoBehaviour
{
    // Event signature
    [System.Serializable] public class TriggerCollider2DEvent : UnityEvent<Collider2D> {}

    #region Public Attributs
    public LayerMask layermask = Physics.DefaultRaycastLayers;
    public float timeBeforeToLaunchTriggerEnter;
    public TriggerCollider2DEvent eventOnTriggerEnter;
    public float timeBeforeToLaunchTriggerStay;
    public TriggerCollider2DEvent eventOnTriggerStay;
    public float timeBeforeToLaunchTriggerExit;
    public TriggerCollider2DEvent eventOnTriggerExit;
    #endregion

    #region Protected Attributs
    #endregion

    #region Private Attributs
    #endregion

    #region Properties
    #endregion

    protected virtual void Start()
    {
    }

    public virtual bool CanTrigger(Collider2D other)
    {
        return enabled && (layermask == (layermask | (1 << other.gameObject.layer)));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (CanTrigger(other))
            StartCoroutine(EventTrigger(timeBeforeToLaunchTriggerEnter, eventOnTriggerEnter, other));
    }
    public virtual void EventOnTriggerEnterZD(Collider2D _other)
    {
        eventOnTriggerEnter.Invoke(_other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (CanTrigger(other))
            StartCoroutine(EventTrigger(timeBeforeToLaunchTriggerStay, eventOnTriggerStay, other));
    }
    public virtual void EventOnTriggerStay2D(Collider2D _other)
    {
        eventOnTriggerEnter.Invoke(_other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (CanTrigger(other))
            StartCoroutine(EventTrigger(timeBeforeToLaunchTriggerExit, eventOnTriggerExit, other));
    }
    public virtual void EventOnTriggerExit2D(Collider2D _other)
    {
        eventOnTriggerEnter.Invoke(_other);
    }

    IEnumerator EventTrigger(float _timer, TriggerCollider2DEvent _event, Collider2D _other)
    {
        yield return new WaitForSeconds(_timer);
        _event.Invoke(_other);
    }
}
