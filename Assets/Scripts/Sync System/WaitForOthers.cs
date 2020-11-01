using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitForOthers : MonoBehaviour
{
    public string nickName = "ToGame";
    public List<ActionInfo> ActionsNeeded = new List<ActionInfo>();

    public UnityEvent OnStartSequence;
    public UnityEvent OnCompleteSequence;

    public bool sequenceCompleted = false;
    private int totalSequence = 0;
    private int currentProgress = 0;

    //Check for repeated
    public void StartSequence()
    {
        print(this.gameObject.name.ToString() + " starting Sequence");

        totalSequence = ActionsNeeded.Count;
        currentProgress = 0;
        sequenceCompleted = false;
        SetGroup();

        foreach (var act in ActionsNeeded)
        {
            act.wait.StartAction();
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
        foreach (var item in ActionsNeeded)
        {
            if (item.wait == obj)
            {
                item.completed = true;
            }
        }

        if (currentProgress >= totalSequence)
            CompleteSequence();
    }

    private void SetGroup()
    {
        foreach (var act in ActionsNeeded)
        {
            act.wait.group = this;
        }
    }

}
[System.Serializable]
public class ActionInfo
{
    public WaitForMe wait;
    public bool completed;
}