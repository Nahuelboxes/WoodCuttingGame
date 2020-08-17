using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public ScreenBehavior startScreen;
    public ScreenBehavior settingScreen;
    public ScreenBehavior gameScreen;
    public ScreenBehavior pauseScreen;
    public ScreenBehavior winLvlScreen;
    public ScreenBehavior looseLvlScreen;

    [Space]
    public GameObject blocker;

    [Space]
    public ScreenBehavior activeScreen;

    public bool canSwap = true;

    public static ScreensManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        activeScreen = startScreen;
        //activeScreen.Show();
    }

    IEnumerator SwapScreen(ScreenBehavior newScreen)
    {
        canSwap = false;
        yield return null;
        ToggleBlocker();
        if (activeScreen != null)
        {
            activeScreen.Hide();

            // yield return new WaitForSeconds(activeScreen.hideTime);
            while (activeScreen.IsPlayingAnim())
            {
                yield return null;
            }

            activeScreen.Deactivate();
        }

        newScreen.Activate();
        newScreen.Show();
        yield return new WaitForSeconds(newScreen.showTime);
        activeScreen = newScreen;
        canSwap = true;
        ToggleBlocker();
    }

    public void DeactivateCurrent()
    {
        activeScreen.Deactivate();
    }

    public void ActivateCurrent()
    {
        activeScreen.Activate();
    }

    public void ShowStartScreen()
    {
        StartCoroutine(SwapScreen(startScreen));
    }

    public void ShowSettingsScreen()
    {
        StartCoroutine(SwapScreen(settingScreen));
    }

    public void ShowGameScreen(bool useCourtutine)
    {
        if (useCourtutine)
            StartCoroutine(SwapScreen(gameScreen));
        else
        {
            activeScreen.Deactivate();
            activeScreen = gameScreen;
            activeScreen.Activate();
        }    
    }

    public void ShowPauseScreen()
    {

    }

    public void ShowStoreScreen()
    {

    }

    public void ShowGoldScreen()
    {

    }

    public void ShowWinLvlScreen()
    {
        StartCoroutine(SwapScreen(winLvlScreen));
    }

    public void ShowLooseLvlScreen()
    {
        StartCoroutine(SwapScreen(looseLvlScreen));
    }

    public void ToggleBlocker(){
     
     blocker.SetActive(blocker.activeInHierarchy ? false: true);
    // blocker
        // if (blocker.activeInHierarchy){
        //     blocker.SetActive(false);
        // }else{
        //     blocker.SetActive(true);
        // }
    }
    

}
