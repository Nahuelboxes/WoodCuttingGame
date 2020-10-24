using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitForMe : MonoBehaviour
{
    public bool actionCompleted = false;
    public UnityEvent OnStarAction;
    [HideInInspector]
    public WaitForOthers group;

    public void StartAction()
    {
        actionCompleted = false;
        OnStarAction.Invoke();
    }

    public void CompleteAction()
    {
        actionCompleted = true;
        group.ActionComplete(this);

    }

}
