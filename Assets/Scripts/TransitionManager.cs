using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionManager : MonoBehaviour
{
    //this one is just for "Go to Game". To Do: Add and array of this for every Transition
    public WaitForOthers syncroSystem;
    [Space]
    public GameObject camObj;

    [Space]
    public List<ContainerInfo> containers = new List<ContainerInfo>();

    [Space]
    public ContainerInfo currContInfo;

    [Header("Transition UI")]
    public AnimatedPartUI trnasitionSreen;

    [Header("Tutorial System")]
    public TutorialAnimSystem tutSystem;

    public static TransitionManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(this); }

        if (containers.Count <= 0)
            Debug.LogError("No containers in Transition Manager!");
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
        if (target == null)
        {
            Debug.LogError("No Target assinged for transitioning! --- PlayMode will be paused");
            Debug.Break();
        }

        StartTransition();
        if (currContInfo != null)
            currContInfo.partCont.StartDeactivation(); //---> Is this necessary?

        yield return new WaitForSeconds(td.waitInCurrent);

        currContInfo.partCont = target;
        MoveCamToCurrent();
        currContInfo.partCont.StartActivation();

        //Syncro
        syncroSystem.StartSequence();
        //if I am moving to GameScreen
        if (currContInfo.cointainersType == CointainersTypes.gameScreen)
        {
            //ask for tutorial stuff
            tutSystem.TryTriggerTutorial(LvlManager.instance.currentGameMode);
            print(LvlManager.instance.currentGameMode.ToString());
        }

     
        while (!syncroSystem.sequenceCompleted)
        {
            yield return null;
        }

      
        yield return new WaitForSeconds(td.waitLoadingMin);

        ComeBackFromTransition();
        yield return new WaitForSeconds(td.waitInNew);

        //REFACTOR this
        //print("Transition has ended");
        HandleArrive(currContInfo.partCont);
    }

    private void HandleArrive(PartContainer cont)
    {
        foreach (var item in containers)
        {
            if (item.partCont == cont)
            {
                item.OnReachScreen?.Invoke();
            }
        }
    }

    public void MoveCamToCurrent()
    {
        //camObj.transform.position = currCont.camPos.transform.position;
        Camera c = camObj.GetComponent<Camera>();
        currContInfo.partCont.camHolder.AdaptCamera(c);
    }

    private PartContainer GetContainerByType(CointainersTypes wantedType)
    {
        foreach (var item in containers)
        {
            if (item.cointainersType == wantedType)
                return item.partCont;
        }
        return null;
    }


    //Jump To Game
    public void JumpToGame()
    {
        TransitionData d = new TransitionData();
        d.waitInCurrent = 1f;
        d.waitLoadingMin = 2f;
        d.waitInNew = 2f;

        currContInfo.cointainersType = CointainersTypes.gameScreen;
        StartCoroutine(MovingTo(GetContainerByType(CointainersTypes.gameScreen), d) );
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

public enum CointainersTypes
{ 
    startScreen,
    gameScreen,
    storeScreen,
}

[System.Serializable]
public class ContainerInfo
{
    public CointainersTypes cointainersType;
    public PartContainer partCont;
    public UnityEvent OnReachScreen;
}

