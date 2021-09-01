using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTriggerWithElements : EventOnTrigger
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

    public override bool CanTrigger(Collider other)
    {
        return base.CanTrigger(other) && objectsCanInteract.Contains(other.gameObject);
    }
}
