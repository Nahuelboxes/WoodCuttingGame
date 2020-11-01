using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPanel : MonoBehaviour
{
    public Animator animator;
    public string showKey;

    private bool showingPanel;

    public void HandleButtomClick()
    {
        if (!showingPanel)
            ShowPanel();
        else 
            HidePanel();
    }

    private void ShowPanel()
    {
        animator.SetBool(showKey, true);
        showingPanel= true;
    }

    private void HidePanel()
    {
        animator.SetBool(showKey, false);
        showingPanel = false;
    }
}
