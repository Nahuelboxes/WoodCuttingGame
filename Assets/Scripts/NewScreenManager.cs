using UnityEngine;
using System.Collections.Generic;

public class NewScreenManager : MonoBehaviour
{
    public List<ScreenInfo> screens;
    public GameObject blocker;
    public GameObject bottomPanel;

    private ScreenInfo currentScreen;

    public static NewScreenManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        currentScreen = screens[0];
    }

    public void ShowScreen(string screenName)
    {
        currentScreen.screen.SetActive(false);
        currentScreen = GetByName(screenName);
        currentScreen.screen.SetActive(true);
    }

    public void DeactiveCurrent()
    {
        currentScreen.screen.SetActive(false);
    }

    public void SetCurrent(string screenName)
    {
        currentScreen = GetByName(screenName);
    }

    public void ShowCurrent()
    {
        currentScreen.screen.SetActive(true);
    }

    public void ActivateBlocker()
    {
        blocker.SetActive(true);
    }

    public void DeactivateBlocker()
    {
        blocker.SetActive(false);
    }

    public void ActivateBottomPanel()
    {
        bottomPanel.SetActive(true);
    }

    public void DeactivateBottomPanel()
    {
        bottomPanel.SetActive(false);
    }

    private ScreenInfo GetByName(string name)
    {
        foreach (ScreenInfo item in screens)
        {
            if (item.nameTag == name)
                return item;
        }

        Debug.LogAssertion("No name match!!");
        return screens[0];
    }
    
}

[System.Serializable]
public class ScreenInfo
{
    public string nameTag;
    public GameObject screen;  
}