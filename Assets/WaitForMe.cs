using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForMe : MonoBehaviour
{
    public WaitForOthers waiterGroup;


   
    void Start()
    {
        GetWaiterGroup();
        SuscribeToGroup();
    }

    public void GetWaiterGroup()
    {
        waiterGroup = GetComponentInParent<WaitForOthers>();
    }

    void SuscribeToGroup()
    {
        waiterGroup.Suscribe();
    }

    public void ConditionCompleted()
    {
        waiterGroup.OneCompleted();
    }


}
