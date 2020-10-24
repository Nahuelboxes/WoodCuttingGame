using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPartUI : MonoBehaviour
{
    public Animator anim;
    public GameObject loadingIndicator;

    public void StartAnimIn()
    {
        anim.SetTrigger("In");
    }

    public void SowLoading()
    {
        loadingIndicator.SetActive(true);
    }

    public void HideLoading()
    {
        loadingIndicator.SetActive(false);
    }

    public void StartAnimOut()
    {
        HideLoading();
        anim.SetTrigger("Out");
    }

}
