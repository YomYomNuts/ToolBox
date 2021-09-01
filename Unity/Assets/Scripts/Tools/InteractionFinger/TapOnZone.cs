using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class TapOnZone : TapObject
{
    #region Public Attributs
    public Collider _Zone;
    #endregion

    #region Private Attributs
    #endregion

    #region Properties
    #endregion

    public override bool CanDoActionTap(LeanFinger _finger)
    {
        RaycastHit[] hits = Physics.RaycastAll(_finger.GetRay());
        bool rayCastOk = false;
        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].collider == _Zone)
            {
                rayCastOk = true;
                break;
            }
        }
        return base.CanDoActionTap(_finger) && rayCastOk;
    }
}
