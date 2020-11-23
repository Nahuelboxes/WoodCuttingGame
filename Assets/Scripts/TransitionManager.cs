using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionManager : MonoBehaviour
{
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

    [Space]
    public string gameScreenName = "GameSceen";

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

    IEnumerator MovingTo(ContainerInfo target, TransitionData td)
    {
        if (target == null)
        {
            Debug.LogError("No Target assinged for transitioning! --- PlayMode will be paused");
            Debug.Break();
            yield break;
        }
        if (target == currContInfo)
        {
            //Debug.LogError("Calling the same Container!");
            yield break;
        }

        NewScreenManager SM = NewScreenManager.instance;
        SM.DeactivateBottomPanel();
        StartTransition();
   
        yield return new WaitForSeconds(td.waitInCurrent);

        SM.ActivateBlocker();
        SM.DeactiveCurrent();

        currContInfo = target;
        MoveCamToCurrent();
        currContInfo.partCont.StartActivation();

        //Syncro
        currContInfo.waitForOthersSystem.StartSequence();
        if (currContInfo.cointainersType == CointainersTypes.gameScreen)
        {
            //ask for tutorial stuff
            tutSystem.TryTriggerTutorial(LvlManager.instance.currentGameMode);
            print(LvlManager.instance.currentGameMode.ToString());
        }

        while (!currContInfo.waitForOthersSystem.sequenceCompleted)
        {
            yield return null;
        }

        if (target.cointainersType != CointainersTypes.gameScreen)
            SM.ActivateBottomPanel();

        SM.SetCurrent(target.screenName);
        SM.ShowCurrent();
   
        yield return new WaitForSeconds(td.waitLoadingMin);
        ComeBackFromTransition();
        yield return new WaitForSeconds(td.waitInNew);

        SM.DeactivateBlocker();

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

    private ContainerInfo GetContainerByType(CointainersTypes wantedType)
    {
        foreach (var item in containers)
        {
            if (item.cointainersType == wantedType)
                return item;
        }
        return null;
    }

    public void JumpToGame()
    {
        TransitionData d = new TransitionData();
        d.waitInCurrent = 1f;
        d.waitLoadingMin = 2f;
        d.waitInNew = 2f;

        StartCoroutine(MovingTo(GetContainerByType(CointainersTypes.gameScreen), d) );
        //currContInfo.cointainersType = CointainersTypes.gameScreen;
    }

    public void JumpToStart()
    {
        TransitionData d = new TransitionData();
        d.waitInCurrent = 1f;
        d.waitLoadingMin = 2f;
        d.waitInNew = 2f;

        StartCoroutine(MovingTo(GetContainerByType(CointainersTypes.startScreen), d));
        //currContInfo.cointainersType = CointainersTypes.startScreen;
    }

    public void JumpToStore()
    {
        TransitionData d = new TransitionData();
        d.waitInCurrent = 1f;
        d.waitLoadingMin = 2f;
        d.waitInNew = 2f;

        StartCoroutine(MovingTo(GetContainerByType(CointainersTypes.storeScreen), d));
        //currContInfo.cointainersType = CointainersTypes.storeScreen;
    }

    public void JumpToMap()
    {
        TransitionData d = new TransitionData();
        d.waitInCurrent = 1f;
        d.waitLoadingMin = 2f;
        d.waitInNew = 2f;

        StartCoroutine(MovingTo(GetContainerByType(CointainersTypes.mapScreen), d));
        //currContInfo.cointainersType = CointainersTypes.mapScreen;
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
    mapScreen,

}

[System.Serializable]
public class ContainerInfo
{
    public CointainersTypes cointainersType;
    public PartContainer partCont;
    public UnityEvent OnReachScreen;
    public WaitForOthers waitForOthersSystem;
    public string screenName;

}

