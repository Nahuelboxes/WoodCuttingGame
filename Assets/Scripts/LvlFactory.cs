using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlFactory : MonoBehaviour
{

    //To Do:
    // Create a difficulty filter
    // Get current lvl by PlayerPref

    [Header("Normal Lvl")]
    [SerializeField] private int targetPerLvl = 10;
    [SerializeField] private float timePerTarget = 50f;

    //[Header("Simon Lvl")]
    //[SerializeField] private int sequenceAddision;

    //[Header("Rush Lvl")]
  
    //Just Add more info to the recieved lvlInfo
    public LevelInfo CreateLvl(LevelInfo  lvlInfo, int index)
    {
        switch (lvlInfo.lvlTypeInfo.lvlType)
        {
            case lvlType.normal:
                lvlInfo.lvlDuration = index * timePerTarget;
                lvlInfo.targetsAmount = 50; // index * targetPerLvl;
                break;

            case lvlType.simon:

                break;

            case lvlType.bonus:

                break;

            default:

                Debug.LogAssertion("Level could not be created!");
                lvlInfo = null;
                break;
        }

        return lvlInfo;
    }
}


[System.Serializable]
public class LevelInfo
{
    public LvlType lvlTypeInfo;
    public int lvlIndex;
    public float lvlDuration = 5f;
    //normal
    public int targetsAmount = 50;

    //Simon
    public int sequenceAmount = 3;

    //waterfall
    public float interval = 0.2f;
    
}


public enum lvlType
{ 
    normal,
    simon,
    cascada,
    frenesi,
    bonus,

}
