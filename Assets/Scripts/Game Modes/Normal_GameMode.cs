using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_GameMode : GameMode_TargetZone
{
    public float timeForNext = 0.1f;
    public int maxAmountTogether = 4;
    public List<GameObject> targetsInScreen = new List<GameObject>();

    private bool inRageMode = false;

    public override void SetUpMode()
    {
        base.SetUpMode();
        for (int i = 0; i < maxAmountTogether; i++)
        {
            CheckIfMoreAreNeeded();
        }
    }

    public override void CleanTargets()
    {
        base.CleanTargets();
        foreach (var item in targetsInScreen)
        {
            item.SetActive(false);
        }
        targetsInScreen.Clear();
    }


    public override void HandleTargetTouch(GameObject targetTouch, out bool correct)
    {
        base.HandleTargetTouch(targetTouch, out correct);

        targetsInScreen.Remove(targetTouch);

        correct = true;

        StartCoroutine(WaitToGetNextTarget());
    }

    public override void HandleStartRage()
    {
        base.HandleStartRage();
        CleanTargets();
        inRageMode = true;
    }

    public override void HandleRageTouch()
    {
        base.HandleRageTouch();
        //int i = GetRandomIndex();
        //targetsInScreen[i].SetActive(false);
        //targetsInScreen.RemoveAt(i);
        //StartCoroutine(WaitToGetNextTarget());
    }

    public override void HandleEndRage()
    {
        base.HandleEndRage();
        inRageMode = false;
        for (int i = 0; i < maxAmountTogether; i++)
        {
            CheckIfMoreAreNeeded();
        }

      
    }

    int GetRandomIndex()
    {
        return Random.Range(0, targetsInScreen.Count);
    }
    
    public override void HandleTreeTouch()
    {
        base.HandleTreeTouch();
        print("In normal mode touching the tree will be punished!!  :(");
        LvlManager.instance.LoseLife();
    }


    public override void EndTree()
    {
        print("in Normal, stop spawning");
    }


    IEnumerator WaitToGetNextTarget()
    {
        float t = 0f;
        while (t <= timeForNext)
        {
            t += Time.deltaTime;
            yield return null;
        }

        if (!inRageMode)
        {
            CheckIfMoreAreNeeded();
        }
    }

    void CheckIfMoreAreNeeded()
    {
        // asuminedo que un bloque es un golpe
        if (targetsInScreen.Count < maxAmountTogether && zone.treeInfo.GetPartsLeft() > targetsInScreen.Count)
        {
            ActivateOneMore();
        }
    }

    void ActivateOneMore()
    {
        var target = GetNewTarget();

        target.SetActive(true);
        targetsInScreen.Add(target);

    }

    GameObject GetNewTarget()
    {
        GameObject s = zone.GetRandomTarget();

        for (int i = 0; i < targetsInScreen.Count; i++)
        {
            if (s == targetsInScreen[i] || s.activeInHierarchy)
            {
                s = GetNewTarget();
            }
        }

        return s;
    }

}
