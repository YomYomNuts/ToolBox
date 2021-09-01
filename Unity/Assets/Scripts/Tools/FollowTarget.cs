using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    #region Public Attributs
    public Transform subject;
    public bool updatePosition = true;
    public bool updateRotation = false;
    public Vector3 offsetPosition;
    #endregion

    #region Private Attributs
    #endregion

    void Start()
    {
        if (subject)
        {
            if (updatePosition)
                transform.position = subject.position
                    + subject.right * offsetPosition.x
                    + subject.up * offsetPosition.y
                    + subject.forward * offsetPosition.z;
            if (updateRotation)
                transform.rotation = subject.rotation;
        }
    }

    void Update()
    {
        if (subject)
        {
            if (updatePosition)
                transform.position = subject.position
                    + subject.right * offsetPosition.x
                    + subject.up * offsetPosition.y
                    + subject.forward * offsetPosition.z;
            if (updateRotation)
                transform.rotation = subject.rotation;
        }
    }
}
