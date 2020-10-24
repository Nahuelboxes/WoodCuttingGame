using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitForOthers : MonoBehaviour
{
    public string nickName = "ToGame";
    public List<WaitForMe> ActionsNeeded = new List<WaitForMe>();

    public UnityEvent OnStartSequence;
    public UnityEvent OnCompleteSequence;

    public bool sequenceCompleted = false;
    private int totalSequence = 0;
    private int currentProgress = 0;

    //Check for repeated
    public void StartSequence()
    {
        totalSequence = ActionsNeeded.Count;
        currentProgress = 0;
        sequenceCompleted = false;
        SetGroup();

        foreach (var act in ActionsNeeded)
        {
            act.StartAction();
        }

        if (ActionsNeeded.Count <= 0)
            CompleteSequence();
    }

    public void CompleteSequence()
    {
        OnCompleteSequence.Invoke();
        sequenceCompleted = true;
    }

    public void ActionComplete(WaitForMe obj)
    {
        currentProgress++;

        if (currentProgress >= totalSequence)
            CompleteSequence();
    }

    private void SetGroup()
    {
        foreach (var act in ActionsNeeded)
        {
            act.group = this;
        }
    }

}
