using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBehavior : MonoBehaviour
{
    public bool focused = false;

    public Animator animator;

    public bool inAnim=false;

    public string triggerShow;
    public float showTime;

    public string triggerHide;
    public float hideTime;


    private float animControlTimer = 0f;

    void Start()
    {
        
    }


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    //show Animation
    public void Show()
    {
        inAnim = true;
        animator.SetTrigger(triggerShow);
        focused = true;

        StartCoroutine(ControlTimeAnim());
    }
    //show Animation
    public void Hide()
    {
        inAnim = true;
        animator.SetTrigger(triggerHide);
        focused = false;

        StartCoroutine(ControlTimeAnim());
    }

    IEnumerator ControlTimeAnim()
    {
        animControlTimer = 0f;
        while (inAnim)
        {
            animControlTimer += Time.deltaTime;

            if (animControlTimer >= 1.5f)
            {
                OnEndAnim();
                break;
            }
            yield return null;
        }
    }


    public void OnEndAnim()
    {
        inAnim = false;
    }

    public bool IsPlayingAnim()
    {
        return inAnim;
    }

    //Just Set Active(true)
    public void Activate()
    {
        this.gameObject.SetActive(true);
    }
    //Just Set Active(false)
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

}
