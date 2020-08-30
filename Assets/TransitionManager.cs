using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionManager : MonoBehaviour
{
    public GameObject camObj;

    [Space]
    public PartContainer menuCont;
    public PartContainer gameCont;

    [Space]
    public PartContainer currCont;


    [Header("Transition UI")]
    public AnimatedPartUI trnasitionSreen;
    //Transition UI

    //public UnityEvent OnLodingGame;
    public UnityEvent OnArriveToGame;

    public static TransitionManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(this); }
    }

    public void StartTransition()
    {
        trnasitionSreen.StartAnimIn();
    }

    public void ComeBackFromTransition()
    {
        trnasitionSreen.StartAnimOut();
    }

    IEnumerator MovingTo(PartContainer target, TransitionData td)
    {
        StartTransition();
        currCont.StartDeactivation();

        yield return new WaitForSeconds(td.waitInCurrent);


        currCont = target;
        MoveCamToCurrent();
        currCont.StartActivation();
        //OnLodingGame?.Invoke();
        yield return new WaitForSeconds(td.waitLoadingMin);

        ComeBackFromTransition();
        yield return new WaitForSeconds(td.waitInNew);

        print("Transition has ended");
        OnArriveToGame?.Invoke();
    }

    public void MoveCamToCurrent()
    {
        //camObj.transform.position = currCont.camPos.transform.position;
        Camera c = camObj.GetComponent<Camera>();
        currCont.camHolder.AdaptCamera(c); 
    }

    //Jump To Game
    public void JumpToGame()
    {
        TransitionData d = new TransitionData();
        d.waitInCurrent = 1f;
        d.waitLoadingMin = 2f;
        d.waitInNew = 2f;

        StartCoroutine(MovingTo(gameCont, d) );
    }


    //Jump Back to Start
    public void JumpToStart()
    { 
        //Simply reload Scene??

    }

   
}
public struct TransitionData
{
    public float waitInCurrent;
    public float waitLoadingMin;
    public float waitInNew;
}

