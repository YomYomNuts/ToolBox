using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class SwipableObject : MonoBehaviour
{
    #region Public Attributs
    public bool activeByDefault;
    public float thresholdSwipe = 10.0f;
    public UnityEvent eventOnSwipe;
    #endregion

    #region Protected Attributs
    protected bool swipeIsActive;
    #endregion

    #region Private Attributs
    #endregion

    #region Properties
    #endregion

    public void Start()
    {
        if (activeByDefault)
            ActivateSwipe();
    }

    public void ActivateSwipe()
    {
        if (!swipeIsActive)
            LeanTouch.OnFingerSwipe += OnFingerSwipe;
        swipeIsActive = true;
    }

    public void DesactivateSwipe()
    {
        if (swipeIsActive)
            LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        swipeIsActive = false;
    }

    public void OnFingerSwipe(LeanFinger _finger)
    {
        Vector2 swipe = _finger.SwipeScreenDelta;
        if (swipe.magnitude > thresholdSwipe)
        {
            DoActionSwipe(_finger);
        }
    }

    public virtual void DoActionSwipe(LeanFinger _finger)
    {
        eventOnSwipe.Invoke();
    }
}
