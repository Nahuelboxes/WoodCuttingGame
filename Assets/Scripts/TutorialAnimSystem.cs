using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimSystem : MonoBehaviour
{
    public GameObject loadingScreen;

    [Header("Tutorials")]
    public TutorialPair normalModeTut;

    [Space]
    public WaitForMe waitForMe;


    private void OnEnable()
    {
        loadingScreen.SetActive(false);

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        SetUpTuts();
    }

    private void SetUpTuts()
    {
        normalModeTut.tut.sys = this;
    }

    public void TryTriggerTutorial(lvlType lvl)
    {
        int lvlInType = SerializationManager.instance.LoadLvlByMode(lvl);
        if (lvlInType <= 1)
        {
            TriggerTutorial(lvl);
        }
        else
        {
            ShowNormalLoadingScreen();
            OnCompleteCurrentTutorial();
        }
    }

    private void TriggerTutorial(lvlType lvl)
    {
        switch (lvl)
        {
            case lvlType.normal:
                normalModeTut.tutCont.SetActive(true);
                normalModeTut.tut.StartTutorial();
                break;
            case lvlType.simon:
                break;
            case lvlType.cascada:
                break;
            case lvlType.frenesi:
                break;
            case lvlType.bonus:
                break;
            default:
                ShowNormalLoadingScreen();
                break;
        }
    }

    public void ShowNormalLoadingScreen()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        loadingScreen.SetActive(true);
    }
    public void OnCompleteCurrentTutorial()
    {
        waitForMe.actionCompleted = true;
        waitForMe.CompleteAction();
    }
}

[System.Serializable]
public class TutorialPair
{
    public GameObject tutCont;
    public Tutorial tut;
}
