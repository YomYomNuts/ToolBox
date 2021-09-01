using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class TapObject : MonoBehaviour
{
    #region Public Attributs
    public bool activeByDefault;
    public UnityEvent eventOnTap;
    #endregion

    #region Protected Attributs
    protected bool tapIsActive;
    #endregion

    #region Private Attributs
    #endregion

    #region Properties
    public bool TapIsActive
    {
        get { return tapIsActive; }
    }
    #endregion

    public void Start()
    {
        if (activeByDefault)
            ActivateTap();
    }

    public void ActivateTap()
    {
        LeanTouch.OnFingerTap += OnFingerTap;
        tapIsActive = true;
    }

    public void DesactivateTap()
    {
        LeanTouch.OnFingerTap -= OnFingerTap;
        tapIsActive = false;
    }

    public virtual bool CanDoActionTap(LeanFinger _finger)
    {
        return true;
    }

    public void OnFingerTap(LeanFinger _finger)
    {
        if (CanDoActionTap(_finger))
            DoActionTap(_finger);
    }

    public virtual void DoActionTap(LeanFinger _finger)
    {
        eventOnTap.Invoke();
    }
}
