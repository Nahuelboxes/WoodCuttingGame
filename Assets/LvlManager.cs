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
    public string lvlIndexSaveName = "MyLastLvlIndex";
    public int lvl_Index=0;
    public LvlFactory factory;
    public LevelInfo currentLevel;
    public List<LevelInfo> lvlsList = new List<LevelInfo>();

    [Header("Tree Stats")]
    public TreeScript currTree;

    [Header("Time")]
    [Range(0f,2f)]public float timerScale = 1f;
    public float timer = 0f;
    public Text lvlTimerText;

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

    //Retry Level
    public void Retry()
    {
        //insert transition
        StartCoroutine(SetUpRety());
        
        

    }

    IEnumerator SetUpRety()
    {
        ScreensManager.instance.ShowGameScreen(true);
        TransitionManager.instance.StartTransition();
        yield return new WaitForSeconds(2f);

        print("player want to reset this level");
        ResumeGame();
        inGame = false;
        InputsManager.instance.DisableTouch();
        CreateLvl();
        myLumberJack.GoToIdle();
        yield return new WaitForSeconds(3f);


        TransitionManager.instance.ComeBackFromTransition();
        TransitionManager.instance.OnArriveToGame.Invoke();
        
    }



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

        //LVL
        //CreateLvl();

        OnStartGame.Invoke();
        //Time
        timer = currentLevel.lvlDuration;

        //Lives
        currLivesAmount = maxLives;
        InitializeLifeUI();
        InputsManager.instance.EnableTouch();
    }

    void GameTick()
    {
        if (isPaused) return;

        //Time
        TimerTick();

        if (timer <= 0f)
        {
            print("Se terminó el tiempo pibe");
            LooseLvl();

        }

    }

    //Game Time and Update time UI
    void TimerTick()
    {
        timer -= Time.deltaTime * timerScale;
        string min = Mathf.Floor(((int)timer / 60)).ToString();
        string seg = Mathf.Floor((timer % 60)).ToString("00");
        string miliSeg = Mathf.Floor((timer * 10 % 10)).ToString("00");

        lvlTimerText.text = min + ":" + seg + ":" + miliSeg;
    }

    //Loose Life
    public void LooseLife()
    {
        currLivesAmount--;
        OnLooseLife?.Invoke();
        UpdateLifeUI();

        if (currLivesAmount <= 0)
        {
            print("Perdiste todas las Vidas");
            LooseLvl();
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

    public void LooseLvl()
    {
        inGame = false;
        OnLooseLvl?.Invoke();
        ScreensManager.instance.ShowLooseLvlScreen();

    }

    public void WinLvl()
    {
        inGame = false;
        OnWinLvl?.Invoke();
        ScreensManager.instance.ShowWinLvlScreen();

        lvl_Index++;
        SaveLvlIndex();
        //Assings Resources
    }

    //TO DO:
    //Set a structure to fill up the end panels.
    #endregion

    #region Lvl Management
    //lvl Set Up  
    public void CreateLvl()
    {
        lvl_Index = LoadLvlIndex();
        currentLevel = factory.CreateLvl(GetLvlFromListByType(lvlType.normal), lvl_Index); //Getting Lvl from factory

        currTree.CreateTree(currentLevel, currentLevel.targetsAmount); //Create the propper tree 
    }

    private int LoadLvlIndex()
    {
        if (PlayerPrefs.HasKey(lvlIndexSaveName))
        {
            int loadedData = PlayerPrefs.GetInt(lvlIndexSaveName);
            if (loadedData <= 0)
            {
                return 1;
            }
            else
            {
                return loadedData;
            }
        }
        else
        {
            return 1;
        }
    }

    private void SaveLvlIndex()
    {
        PlayerPrefs.SetInt(lvlIndexSaveName, lvl_Index);
        PlayerPrefs.Save();
        
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
