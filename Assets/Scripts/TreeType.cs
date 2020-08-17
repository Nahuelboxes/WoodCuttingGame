using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type",menuName = "ScriptableObject/TreeTypes")]
public class TreeType : ScriptableObject
{
    public string Name;
    public List<Sprite> parts;
    public float partHeight;
    public int hitsToDestroyLog=1;
}
