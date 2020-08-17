using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitForOthers : MonoBehaviour
{
    [Header("Others")]
    //public int amount = 0;
    //public bool[] conditions;
    public int totalConditions=0;
    public int aprovedConditions=0;

    [Space]
    public UnityEvent OnReady;


    public void StartWait()
    {
        StartCoroutine(ValidateOthers());
        
    }

    IEnumerator ValidateOthers()
    {
        yield return null;
        while (!ConditionsReady() )
        {
            yield return null;
        }

        OnReady.Invoke();
    }

    bool ConditionsReady()
    {
        if (aprovedConditions >= totalConditions)
            return true;
        else 
            return false;
    }

    //bool GetAllValues()
    //{
    //    for (int i = 0; i < conditions.Length; i++)
    //    {
    //        if (!conditions[i])
    //            return false;
    //    }

    //    return true;
    //}


    public void Suscribe()
    {
        //amount++;
        totalConditions++;
    }

    public void Unsubscribe()
    {
        //amount--;
        totalConditions--;
    }

    public void OneCompleted()
    {
        aprovedConditions++;
    }

}
