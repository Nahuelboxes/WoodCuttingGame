using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameMode_TargetZone : MonoBehaviour
{
    protected TargetZoneSpawner zone;
    public string modeName;

    public virtual void SetUpMode()
    {
        print("Setting up: " + modeName);
        zone = this.GetComponent<TargetZoneSpawner>();
    }

    public virtual void HandleTargetTouch(GameObject targetTouch, out bool correct)
    {
        correct = false;
        print("handle target touch as: " + modeName);
    }

    public virtual void HandleTreeTouch()
    {
        print("handle tree touch as: " + modeName);
    }

    public virtual void EndTree()
    {
        print("Tree ended: " + modeName);
    }
}
