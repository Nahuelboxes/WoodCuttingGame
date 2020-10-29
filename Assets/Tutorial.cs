using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tutorial : MonoBehaviour
{
    public Button continueButton;
    public Button skipButton;

    public int stepIndex = 0;
    public GameObject[] tutConts;

    public TutorialAnimSystem sys;

    protected virtual void SetUp()
    {
        for (int i = 0; i < tutConts.Length; i++)
        {
            tutConts[i].SetActive(false);
        }
        stepIndex = 0;
        continueButton.onClick.AddListener(NextStep);
        skipButton.onClick.AddListener(CompleteTutorial);
    }


    public virtual void StartTutorial()
    {
        this.SetUp();
        stepIndex = 0;
        tutConts[stepIndex].SetActive(true);
    }

    public virtual void NextStep()
    {
        tutConts[stepIndex].SetActive(false);
        if (stepIndex < tutConts.Length-1)
        {
            stepIndex++;
            tutConts[stepIndex].SetActive(true);
        }
        else 
        {
            CompleteTutorial();
        }
    }

    public virtual void CompleteTutorial()
    {
        for (int i = 0; i < tutConts.Length; i++)
        {
            tutConts[i].SetActive(false);
        }

        sys.OnCompleteCurrentTutorial();
        sys.ShowNormalLoadingScreen();

        continueButton.onClick.RemoveListener(NextStep);
        skipButton.onClick.RemoveListener(CompleteTutorial);
    }
}
