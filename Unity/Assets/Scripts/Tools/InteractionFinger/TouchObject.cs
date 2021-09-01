using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class TouchObject : MonoBehaviour
{
    #region Public Attributs
    public bool activeByDefault;
    public UnityEvent eventOnTouch;
    #endregion

    #region Protected Attributs
    protected bool touchIsActive;
    #endregion

    #region Private Attributs
    #endregion

    #region Properties
    #endregion

    public void Start()
    {
        if (activeByDefault)
            ActivateTouch();
    }

    public void ActivateTouch()
    {
        LeanTouch.OnFingerDown += OnFingerTouch;
        touchIsActive = true;
    }

    public void DesactivateTouch()
    {
        LeanTouch.OnFingerDown -= OnFingerTouch;
        touchIsActive = false;
    }

    public virtual bool CanDoAction(LeanFinger _finger)
    {
        return true;
    }

    public void OnFingerTouch(LeanFinger _finger)
    {
        if (CanDoAction(_finger))
            DoAction(_finger);
    }

    public virtual void DoAction(LeanFinger _finger)
    {
        eventOnTouch.Invoke();
    }
}
