using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToDestination : MonoBehaviour
{
    #region Public Attributs
    public Transform subject;
    public Transform target;
    public Animator animator;
    public bool activeMovementAnim = true;
    public float speed;
    public bool stopWhenReach;
    public UnityEvent eventOnFinish;
    #endregion

    #region Delegate
    public delegate void DelegateOnFinish();
    public DelegateOnFinish delegateOnFinish;
    #endregion

    #region Private Attributs
    private int idMove = Animator.StringToHash("Move");
    private Vector3 subjectStartingPosition;
    #endregion

    #region Properties
    #endregion

    void Start()
    {
        subjectStartingPosition = subject.transform.position;
    }

    void FixedUpdate()
    {
        float step =  speed * Time.deltaTime;

        if (animator != null && activeMovementAnim)
            animator.SetFloat(idMove, speed);

        if (target != null)
        {
            subject.position = Vector3.MoveTowards(subject.position, target.position, step);
            if (Vector3.Distance(subject.position, target.position) < 0.001f)
            {
                if (stopWhenReach)
                {
                    Stop();
                    eventOnFinish.Invoke();
                    if (delegateOnFinish != null)
                        delegateOnFinish();
                }
            }
        }
    }

    public void DefineNewTarget(Transform _target)
    {
        target = _target;
    }

    public void Stop()
    {
        if (animator != null)
            animator.SetFloat(idMove, 0.0f);
        enabled = false;
    }

    public void DefinePosition(Transform _target)
    {
        subject.position = target.position;
    }

    public void ResetValues()
    {
        subject.transform.position = subjectStartingPosition;
    }
}
