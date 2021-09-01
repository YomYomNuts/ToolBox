using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger2DWithElements : EventOnTrigger2D
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

    protected override void Start()
    {
        base.Start();
    }

    public override bool CanTrigger(Collider2D other)
    {
        return base.CanTrigger(other) && (objectsCanInteract.Count == 0 || objectsCanInteract.Contains(other.gameObject));
    }
}
