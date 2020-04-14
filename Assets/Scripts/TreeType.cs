using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type",menuName = "ScriptableObject/TreeTypes")]
public class TreeType : ScriptableObject
    {
    public  string Name;
    public List<Sprite> parts;
    public int hitsToDestroyLog;
    }
