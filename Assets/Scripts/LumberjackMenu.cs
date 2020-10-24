using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackMenu : MonoBehaviour
{
    public Animator anim;
    public string triggerJump;

    [ContextMenu ("Test Jump")]
    public void StartJump()
    {
        anim.SetTrigger(triggerJump);
    }
}
