using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LvlManager : MonoBehaviour
{
    [Header("Count Down")]
    public bool inCountDown = false;
    public int countDownStart = 3;
    private int countDownCurrNumber;
    public float countdown_NumberTime = 0.5f;
    private float countDownTimer = 0f;
    public Text countdownText;
    public GameObject countdownPanel;

    [Header("Game")]
    public bool inGame = false;
    public GameObject gamePanel;
    public bool isPaused = false;
    public GameObject pausePanel;

    [Header("Lvl Info")]
    public lvlType currentGameMode;
    private int lvl_Index=1;
    public LvlFactory factory;
    public LevelInfo currentLevel;
    public List<LevelInfo> lvlsList = new List<LevelInfo>();

    [Header("Tree Stats")]
    public TreeScript currTree;

    [Header("Tree UI")]
    public Text logsLeftText;
    public Text totalLogsText;

    [Header("Time")]
    [Range(0f,2f)]public float timerScale = 1f;
    public float timer = 0f;
    public Text lvlTimerText;

    [Header("Rage")]
    public bool canGetRage = true;
    public bool inRageMode = false;
    public float rageDuration = 5f;
    public float rageChargePerHit = 0.3f; // in percentage
    public float rageLoseOverTime = 0.5f; //in percentage per minute
    public float currRageAmount = 0f; // current rage amount in percentage
    public RageBarPresenter rageBarPresenter;

    [Header("Lumberjack")]
    public Lumberjack myLumberJack;
    public int maxLives=3;
    public int currLivesAmount;
    public GameObject lifeContainer;
    public GameObject lifeObj;
    public List<GameObject> lifeImgs;

    [Header("End Lvl Panel")]
    public Text holi;

    public UnityEvent OnStartGame;
    public UnityEvent OnLooseLife;
    public UnityEvent OnLooseLvl;
    public UnityEvent OnWinLvl;

    public static LvlManager instance;
    private void Awake()
    {
        if (instance == null)
        {instance = this;}
        else 
        { Destroy(this); }
    }


    private void Update()
    {
        if (inCountDown)
        {
            CountDownTick();
        }

        if (inGame)
        {
            GameTick();

            //Rage
            UpdateRage();
        }
    }

    #region CountDown
    //Count Down
    public void StartCountdown()
    {
        InputsManager.instance.DisableTouch();
        countdownPanel.SetActive(true);
        gamePanel.SetActive(false);

        inCountDown = true;
        countDownCurrNumber = countDownStart;
        countdownText.text = countDownCurrNumber.ToString();

        CountDownTick();
    }

    public void CountDownTick()
    {
        countDownTimer += Time.deltaTime;
        if (countDownTimer >= countdown_NumberTime)
        {
            countDownTimer = 0f;
            countDownCurrNumber--;
            countdownText.text = countDownCurrNumber.ToString();
            //Trigger Animation of countdownText
            if (countDownCurrNumber == 1){
            //StartGame for spawning Animation
            myLumberJack.StartGame();
            }
            if (countDownCurrNumber <= 0)
            {
                StartLvl();
            }
        }
    }
    #endregion

    #region Pause
    //Pause --> Make this whith the ScreenManager
    public void PuaseGame()
    {
        isPaused = true;

        gamePanel.SetActive(false);
        pausePanel.SetActive(true);

        InputsManager.instance.DisableTouch();
    }
    //To Do: add a countdown when returning from pause
    public void ResumeGame()
    {
        isPaused = false;

        gamePanel.SetActive(true);
        pausePanel.SetActive(false);

        print("Resume Game");
        InputsManager.instance.EnableTouch();
    }

    public void ResumeGameAfter(float seconds)
    {
        StartCoroutine(WaitForResumeGame(seconds));
    }
    IEnumerator WaitForResumeGame(float s)
    {
        yield return new WaitForSeconds(s);
        isPaused = false;

        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    #endregion

    #region Game
    //Game Logic
    public void StartLvl()
    {
        //Start game
        inCountDown = false;
        inGame = true;
        //Panels
        countdownPanel.SetActive(false);
        gamePanel.SetActive(true);

        OnStartGame.Invoke();
        //Time
        timer = currentLevel.lvlDuration;

        //Logs
        totalLogsText.text = currTree.TotalTreeSizeInParts.ToString();
        logsLeftText.text = currTree.TotalTreeSizeInParts.ToString();

        //Lives
        currLivesAmount = maxLives;
        InitializeLifeUI();
        InputsManager.instance.EnableTouch();

        //Rage
        currRageAmount = 0f;
    }

    void GameTick()
    {
        if (isPaused) return;

        //Time
        GameTimerTick();

        if (timer <= 0f)
        {
            print("Se terminó el tiempo pibe");
            LoseLvl();

        }

    }

    //Game Time and Update time UI
    void GameTimerTick()
    {
        timer -= Time.deltaTime * timerScale;
        string min = Mathf.Floor(((int)timer / 60)).ToString();
        string seg = Mathf.Floor((timer % 60)).ToString("00");
        string miliSeg = Mathf.Floor((timer * 10 % 10)).ToString("00");

        lvlTimerText.text = min + ":" + seg + ":" + miliSeg;

        if (timer < 0f)
        {
            timer = 0f;
            lvlTimerText.text = "00:00:00";
        }

    }

    public void StopGameTimeFor(float seconds)
    {
        StartCoroutine(StopGameTimerForAWhile(seconds));
    }

    IEnumerator StopGameTimerForAWhile(float interval)
    {
        float t = 0;
        float currTimeScale = timerScale;
        timerScale=0f;
        while (t < interval)
        {
            t += Time.deltaTime;
            yield return null;
        }
        timerScale = currTimeScale;
    }

    //Correct Hit
    public void OnCorrectHit()
    {
        //Update Tree UI
        logsLeftText.text = currTree.GetPartsLeft().ToString ();
        if (inRageMode) 
        {
            myLumberJack.RageHit();
            return; 
        }

        currRageAmount += rageChargePerHit;

        if (currRageAmount >= 1) //Activate Rage
        {
            currRageAmount = 1;
            ActivateRageMode();
        }
    }

    //RAGE
    public void ActivateRageMode()
    {
        inRageMode = true;
        currTree.HandleStartRage();
        rageBarPresenter.ActivateRage();

        //TO DO: Stop timer and input for the animation
        myLumberJack.StartRageMode();
        StopGameTimeFor(myLumberJack.rageStartDuration);
    }

    void UpdateRage()
    {
        if (inRageMode)
        {
            currRageAmount -= rageDuration * Time.deltaTime / 60f;
            if (currRageAmount <= 0)
            {
                EndRageMode();
            }
        }
        else
        {
            if(currRageAmount >0)
                currRageAmount -= rageLoseOverTime * Time.deltaTime;
        }
        currRageAmount = Mathf.Clamp(currRageAmount, 0f, 1f);
        rageBarPresenter.UpdateBar(currRageAmount);
    }

    public void EndRageMode()
    {
        inRageMode = false;
        currRageAmount = 0f;
        currTree.HandleEndRage();

        rageBarPresenter.EndRage();

        //TO DO: Stop timer and input for the animation
        myLumberJack.EndRage();
        StopGameTimeFor(myLumberJack.rageEndingDuration);

    }

    //Lose Life
    public void LoseLife()
    {
        currLivesAmount--;
        OnLooseLife?.Invoke();
        UpdateLifeUI();

        if (currLivesAmount <= 0)
        {
            VibrationManager.instance.Vibrate(vibrationDuration.large);
            print("Perdiste todas las Vidas");
            LoseLvl();
        }
    }
    
    public void InitializeLifeUI()
    {
        foreach (var item in lifeImgs)
        {
            Destroy(item.gameObject);
        }
        lifeImgs.Clear();

        for (int i = 0; i < maxLives; i++)
        {
            GameObject l = Instantiate(lifeObj,lifeContainer.transform);
            lifeImgs.Add(l);
        }

    }

    public void UpdateLifeUI()
    {
        for (int i = 0; i < lifeImgs.Count; i++)
        {
            if (i > currLivesAmount-1)
            {
                lifeImgs[i].SetActive(false);
            }
        }
    }

    public void LoseLvl()
    {
        inGame = false;
        OnLooseLvl?.Invoke();
        currTree.targetZone.ClearTreeTargets();
        //ScreensManager.instance.ShowLooseLvlScreen();
        InputsManager.instance.DisableTouch();
    }

    public void WinLvl()
    {
        inGame = false;
        OnWinLvl?.Invoke();
        //ScreensManager.instance.ShowWinLvlScreen();
        InputsManager.instance.DisableTouch();

        lvl_Index++;
        SerializationManager.instance.SaveLvlByMode(currentGameMode, lvl_Index);
        //Assings Resources
    }

    //Retry Level
    public void Retry()
    {
        StartCoroutine(SetUpRety());
    }

    IEnumerator SetUpRety()
    {
        //ScreensManager.instance.ShowGameScreen(true);
        TransitionManager.instance.StartTransition();
        yield return new WaitForSeconds(2f);

        ResumeGame();
        inGame = false;
        InputsManager.instance.DisableTouch();
        CreateLvl();
        myLumberJack.GoToIdle();
        yield return new WaitForSeconds(3f);


        TransitionManager.instance.ComeBackFromTransition();
        StartCountdown();

        currRageAmount = 0f;
        UpdateRage();

    }
    //TO DO:
    //Set a structure to fill up the end panels.


    #endregion

    #region Lvl Management
   
    public void SetGameMode(lvlType type) //Unused
    {
        currentGameMode = type;
    }

    public void SetLastPlayedMode()
    {
        currentGameMode = SerializationManager.instance.LoadLastGameModePlayed();
        lvl_Index = SerializationManager.instance.LoadLvlByMode(currentGameMode);
    }

    public void SetNormalGameMode()
    {
        currentGameMode = lvlType.normal;
        lvl_Index = SerializationManager.instance.LoadLvlByMode(currentGameMode);
    }
    public void SetSimonGameMode()
    {
        currentGameMode = lvlType.simon;
        lvl_Index = SerializationManager.instance.LoadLvlByMode(currentGameMode);
    }


    public void CreateLvl()
    {
        currentLevel = factory.CreateLvl(GetLvlFromListByType(currentGameMode), lvl_Index);
        //print("Current Tree: " + (currTree != null));
        currTree.CreateTree(currentLevel, currentLevel.targetsAmount); //Create the propper tree 

        SerializationManager.instance.SaveLastGameModePlayed(currentGameMode);
    }
   
    private LevelInfo GetLvlFromListByType(lvlType lvlTypeEnum)
    {
        foreach (var t in lvlsList)
        {
            if (t.lvlTypeInfo.lvlType == lvlTypeEnum)
            {
                return t;
            }
        }

        return lvlsList[0];
    }
    #endregion
}
