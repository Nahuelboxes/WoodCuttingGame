using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PartContainer : MonoBehaviour
{
    public WaitForMe syncroAction;
    [Space]
    public CameraHolder camHolder;

    public bool isReadyToShow = false;

    public UnityEvent OnStartActivation;
    public float activationTime = 1f;
    public UnityEvent OnActivate;
    public UnityEvent OnStartDeactivation;
    public float deactivationTime = 1f;
    public UnityEvent OnDeactivate;

    public bool isFocused = false;

    //Activation
    public void StartActivation()
    {
        isReadyToShow = false;
        this.gameObject.SetActive(true);
        OnStartActivation.Invoke();
        StartCoroutine(WaitForActivation());
    }

    IEnumerator WaitForActivation()
    {
        yield return null;
        yield return new WaitForSeconds(activationTime);
        Activate();
    }

    public void Activate()
    {
        if (syncroAction != null) syncroAction.CompleteAction();
        isReadyToShow = true;
        OnActivate.Invoke();
       
    }



    //Deactivation
    public void StartDeactivation()
    {
        OnStartDeactivation.Invoke();
        StartCoroutine(WaitForDeactivation());
    }

    IEnumerator WaitForDeactivation()
    {
        yield return null;
        yield return new WaitForSeconds(deactivationTime);
        Deactivate();
    }

    void Deactivate()
    {
        OnDeactivate.Invoke();
        this.gameObject.SetActive(false);
    }

    //public void Hide()
    //{
    //    if (isFocused)
    //    { 
    //        OnHide.Invoke();
    //        isFocused = false;

    //        this.gameObject.SetActive(false);
    //    }
    //}

    //public void Hide(float delay)
    //{
    //    StartCoroutine(WaitForHide(delay) );
    //}

    //IEnumerator WaitForHide(float waitTime)
    //{
    //    yield return null;
    //    yield return new WaitForSeconds(waitTime);
    //    Hide();
    //}

    //public void Activate()
    //{
    //    this.gameObject.SetActive(true);
    //    OnActive.Invoke();

    //    isFocused = true;
    //}
}

