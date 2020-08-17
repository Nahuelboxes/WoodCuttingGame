using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays_GameMode : GameMode_TargetZone
{
    public int minSequenceSize = 3;
    public int maxSequenceSize = 5;
    public int sequenceSize;
    public List<Target> currSequence = new List<Target>();
    public int indexSequenceTouch = 0;
    public bool showignSequence = false;
    public float intervalToShow = 0.1f;
    public bool showNumbers = false;

    public override void SetUpMode()
    {
        base.SetUpMode();
        GetNewSequence();
    }

    public override void HandleTargetTouch(GameObject targetTouch, out bool correct)
    {
        base.HandleTargetTouch(targetTouch, out correct);

        correct = CheckOrder(targetTouch.GetComponent<Target>());

        if (!correct)
            LvlManager.instance.LooseLife();

    }

    public override void HandleTreeTouch()
    {
        print("In Simon Mode touching the tree is not punished!");
        return;
    }

    public override void EndTree()
    {
        base.EndTree();
        ClearSequence();

    }


    public void GetNewSequence()
    {
        ClearSequence();

        sequenceSize = GetSequenceSize();

        for (int i = 0; i < sequenceSize; i++)
        {
            int ind = zone.GetRandomIndex();
            bool repeated = false;
            for (int j = 0; j < currSequence.Count; j++)
            {
                if (ind == currSequence[j].index)
                {
                    i--;
                    repeated = true;
                    continue;
                }
            }
            if (!repeated)
            { currSequence.Add(zone.targets[ind].GetComponent<Target>()); }
        }

        indexSequenceTouch = 0;


        ///for Test
        ShowSequence();
    }

    int GetSequenceSize()
    {
        int size = Random.Range(minSequenceSize, maxSequenceSize);

        if (zone.treeInfo.GetPartsLeft() <= minSequenceSize + size)
        {
            size = zone.treeInfo.GetPartsLeft() - 1;
            return size;
        }

        if (zone.treeInfo.GetPartsLeft() - 1 > size)
        {
            return size;
        }
        else
        {
            print(zone.treeInfo.GetPartsLeft() );
            return zone.treeInfo.GetPartsLeft();
        }
    }

    public void ClearSequence()
    {
        foreach (var item in currSequence)
        {
            item.gameObject.SetActive(false);
        }
        currSequence.Clear();
    }

    public void ShowSequence()
    {
        showignSequence = true;
        StartCoroutine(ShowSlowly());
    }

    IEnumerator ShowSlowly()
    {
        yield return new WaitForSeconds(intervalToShow);

        for (int i = 0; i < currSequence.Count; i++)
        {
            if (showNumbers)
            {
                currSequence[i].gameModeSelected = gameMode.simon;
                currSequence[i].simonOrder = i + 1;
            }

            currSequence[i].gameObject.SetActive(true);

            yield return new WaitForSeconds(intervalToShow);
        }
        showignSequence = false;
    }

    bool CheckOrder(Target targetTouch)
    {
        if (targetTouch == currSequence[indexSequenceTouch])
        {
            print("Correct!");
            indexSequenceTouch++;

            if (indexSequenceTouch >= currSequence.Count)
            {
                print("sequencia completa!");
                GetNewSequence();

            }

            return true;
        }
        else
        {
            print("WORNG!");
            //Wait for a bit maybe?
            GetNewSequence();

            return false;
        }
    }

}
