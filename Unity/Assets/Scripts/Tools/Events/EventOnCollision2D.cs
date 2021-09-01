using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnCollision2D : MonoBehaviour
{
    // Event signature
    [System.Serializable] public class CollisionCollider2DEvent : UnityEvent<Collider2D> {}

    #region Public Attributs
    public LayerMask layermask = Physics.DefaultRaycastLayers;
    public float timeBeforeToLaunchCollisionEnter;
    public CollisionCollider2DEvent eventOnCollisionEnter;
    public float timeBeforeToLaunchCollisionStay;
    public CollisionCollider2DEvent eventOnCollisionStay;
    public float timeBeforeToLaunchCollisionExit;
    public CollisionCollider2DEvent eventOnCollisionExit;
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

    public virtual bool CanCollision(Collision2D other)
    {
        return ((layermask >> other.gameObject.layer) | 1) == 1;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (CanCollision(other))
            StartCoroutine(EventCollision(timeBeforeToLaunchCollisionEnter, eventOnCollisionEnter, other));
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (CanCollision(other))
            StartCoroutine(EventCollision(timeBeforeToLaunchCollisionStay, eventOnCollisionStay, other));
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (CanCollision(other))
            StartCoroutine(EventCollision(timeBeforeToLaunchCollisionExit, eventOnCollisionExit, other));
    }

    IEnumerator EventCollision(float _timer, CollisionCollider2DEvent _event, Collision2D _other)
    {
        yield return new WaitForSeconds(_timer);
        _event.Invoke(_other.collider);
    }
}
