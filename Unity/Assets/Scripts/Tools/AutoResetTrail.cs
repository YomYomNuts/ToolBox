using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoResetTrail : MonoBehaviour
{
    #region Public Attributs
    public TrailRenderer _TrailRenderer;
    #endregion

    #region Private Attributs
    #endregion

    void OnDisable()
    {
        _TrailRenderer.Clear();
    }
}
