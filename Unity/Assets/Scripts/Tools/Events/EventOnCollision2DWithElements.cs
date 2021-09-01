using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnCollision2DWithElements : EventOnCollision2D
{
    #region Public Attributs
    public List<GameObject> objectsCanInteract;
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

    public override bool CanCollision(Collision2D other)
    {
        return base.CanCollision(other) && objectsCanInteract.Contains(other.gameObject);
    }
}
