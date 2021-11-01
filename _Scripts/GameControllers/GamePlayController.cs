using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    [SerializeField]
    private GameObject[] topAndBottomBricks, leftBricks, rightBricks;

    public GameObject panelBG, levelFinsishedPanel, playerDiedpanel, pausePanel;

    private GameObject topBrick, bottomBrick, leftBrick, rightBrick;

    private Vector3 coodinates;

    [SerializeField]
    private GameObject[] players;


    public float levelTime;

    public Text liveText, scoreText, levelTimerText, showScoreAtTheEndOfLevel, countDownAndBeginLevelText, watchVideoText;

    private float countDownBeforeLevelBegins = 3.0f;

    public  int smallBallsCount;
   

    public int playerLives, playerScore, coins;

    public bool isGamePaused, hasLevelBegan, levelInProgress, countDownLevel;

    [SerializeField]
    private GameObject[] endOfLevelReward;

    [SerializeField] private Button pauseBtn;
 
    void Awake()
    {
        CreateInstance();
      //  InitializeBricksAnsPlayer();
    }

    private void Start()
    {
        InitializeGameplayController();
    }
  
    void Update()
    {
        UpdateGameplayController();
    }
    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void InitializeBricksAnsPlayer()
    {
        coodinates = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

     
        int index = UnityEngine.Random.Range(0, topAndBottomBricks.Length);

        topBrick = Instantiate(topAndBottomBricks[index]);
        bottomBrick = Instantiate(topAndBottomBricks[index]);
        leftBrick = Instantiate(leftBricks[index], new Vector3(0,0,0), Quaternion.Euler(0,0, -90)) as GameObject;
        rightBrick = Instantiate(rightBricks[index], new Vector3(0,0,0), Quaternion.Euler(0,0, 90)) as GameObject;

        topBrick.tag = "TopBrick";
        bottomBrick.tag = "BottomBrick";
        leftBrick.tag = "LeftBrick";
        rightBrick.tag = "RightBrick";

        topBrick.transform.position = new Vector3(-coodinates.x + 8, coodinates.y + 0.17f, 0);
        bottomBrick.transform.position = new Vector3(-coodinates.x + 8, -coodinates.y + 1.82f, 0);
        leftBrick.transform.position = new Vector3(-coodinates.x - 0.17f, coodinates.y + 5, 0);
        rightBrick.transform.position = new Vector3(coodinates.x + 0.17f, coodinates.y + 5, 0);

        Instantiate(players[GameController.instance.selectedPlayer]);
    }

    void InitializeGameplayController()
    {
        if (GameController.instance.isGameStartedFromLevelMenu)
        {

            playerLives = 5;
            playerScore = 0;
            GameController.instance.currentScore = playerScore;
            GameController.instance.currentLives = playerLives;
            GameController.instance.isGameStartedFromLevelMenu = false;

            scoreText.text = "Score x" + playerScore;
            liveText.text = "x" + playerLives;
        }
        else
        {
            playerScore = GameController.instance.currentScore;
            playerLives = GameController.instance.currentLives;

            levelTimerText.text = levelTime.ToString("F0");
            scoreText.text = "Score x" + playerScore;
            liveText.text = "x" + playerLives;

            Time.timeScale = 0;
            countDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");

        }
    }

    void UpdateGameplayController()
    {
        scoreText.text = "Score x" + playerScore;

        if (hasLevelBegan)
        {
            CountDownAndBeginLevel();
        }

        if (countDownLevel)
        {
            LevelCountdownTimer();
        }
    }
    public void setHasLevelBegan(bool hasLevelBegan)
    {
        this.hasLevelBegan = hasLevelBegan;
    }

    void CountDownAndBeginLevel()
    {
        countDownBeforeLevelBegins -= (0.19f * 0.15f);
        countDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");
        if(countDownBeforeLevelBegins <= 0)
        {
            Time.timeScale = 1;
            hasLevelBegan = false;
            levelInProgress = true;
            countDownLevel = true;
            countDownAndBeginLevelText.gameObject.SetActive(false);
        }

    }

    void LevelCountdownTimer()
    {
        if(Time.timeScale == 1)
        {
            levelTime -= Time.deltaTime;
            levelTimerText.text = levelTime.ToString("F0");

            if(levelTime <= 0)
            {
                playerLives--;
                GameController.instance.currentLives = playerLives;
                GameController.instance.currentScore = playerScore;

                if(playerLives  < 0)
                {
                    StartCoroutine(PromptTheUserToWatchVIdeo());
                }
                else
                {
                    StartCoroutine(PlayerDiedRestartlevel());
                }
            }
        }
    }

    IEnumerator PlayerDiedRestartlevel()
    {
        levelInProgress = false;

        coins = 0;
        smallBallsCount = 0;

        Time.timeScale = 0;
       
        if(LoadingScreen.instance != null)
        {
            LoadingScreen.instance.FadeOut();
        }

        yield return StartCoroutine(MyCoroutine.waitForRealsec(1.5f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayFadeInAnimation();
        }
    }

    public void PlayerDied()
    {
        countDownLevel = false;
        pauseBtn.interactable = false;
        levelInProgress = false;

        smallBallsCount = 0;

        playerLives--;
        GameController.instance.currentLives = playerLives;
        GameController.instance.currentScore = playerScore;

        if (playerLives < 0)
        {
            StartCoroutine(PromptTheUserToWatchVIdeo());
        }
        //else
        //{
        //    StartCoroutine(PlayerDiedRestartlevel());
        //}

    }

    IEnumerator LevelCompleted()
    {
        countDownLevel = false;
        pauseBtn.interactable = false;

        int unlockedLevel = GameController.instance.currentLevel;
        unlockedLevel++;

        if(!(unlockedLevel >= GameController.instance.levels.Length))
        {
            GameController.instance.levels[unlockedLevel] = true;
           
        }

        Instantiate(endOfLevelReward[GameController.instance.currentLevel], new Vector3(0, Camera.main.orthographicSize, 0), Quaternion.identity);

        if (GameController.instance.doubleCoins)
        {
            coins *= 2;
        }

        GameController.instance.coins = coins;
        GameController.instance.Save();

        yield return StartCoroutine(MyCoroutine.waitForRealsec(4f));

        levelInProgress = false;
        PlayerScript.instance.StopMoving();
        Time.timeScale = 0;

        levelFinsishedPanel.SetActive(true);
        showScoreAtTheEndOfLevel.text = "" + playerScore;
    }

    IEnumerator PromptTheUserToWatchVIdeo()
    {
        levelInProgress = false;
        countDownLevel = false;
        pauseBtn.interactable = false;

        Time.timeScale = 0;

        yield return StartCoroutine(MyCoroutine.waitForRealsec(.7f));

        playerDiedpanel.SetActive(true);
    }
    public void CountSmallBalls()
    {
        smallBallsCount--;

        if(smallBallsCount == 0)
        {
            StartCoroutine(LevelCompleted());
        }
    }

    public void GOToMapBtn()
    {
        GameController.instance.currentScore = playerScore;


        if(GameController.instance.highScore < GameController.instance.currentScore)
        {
            GameController.instance.highScore = GameController.instance.currentScore;
            GameController.instance.Save();
        }

        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene("MainMenu");

        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayLoadingScreen();
        }
     

    }

    public void RestartCurrentLevelBtn()
    {
        smallBallsCount = 0;
        coins = 0;

        GameController.instance.currentScore = playerScore;
        GameController.instance.currentLives = playerLives;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayLoadingScreen();
        }
    }

    public void NextLevelBtn()
    {
        GameController.instance.currentScore = playerScore;
        GameController.instance.currentLives = playerLives;


        if (GameController.instance.highScore < GameController.instance.currentScore)
        {
            GameController.instance.highScore = GameController.instance.currentScore;
            GameController.instance.Save();
        }

        int nextLevel = GameController.instance.currentLevel;
        nextLevel++;

        if(!(nextLevel >= GameController.instance.levels.Length))
        {
            GameController.instance.currentLevel = nextLevel;

            SceneManager.LoadScene("Level" + nextLevel);

            if (LoadingScreen.instance != null)
            {
                LoadingScreen.instance.PlayLoadingScreen();
            }
        }
    }

    public void PauseBtn()
    {
        if (!hasLevelBegan)
        {
            if (levelInProgress)
            {
                if (!isGamePaused)
                {
                    countDownLevel = false;
                    levelInProgress = false;
                    isGamePaused = true;

                    panelBG.SetActive(true);
                    pausePanel.SetActive(true);

                    Time.timeScale = 0;
                }
            }
        }
    }

    public void ResumeBtn()
    {
        countDownLevel = true;
        levelInProgress = true;
        isGamePaused = false;

        panelBG.SetActive(false);
        pausePanel.SetActive(false);

        Time.timeScale = 1;
    }


    IEnumerator GivePlayerLivesRewardAfterWatchingVideo()
    {
        watchVideoText.text = "Thank";

        yield return StartCoroutine(MyCoroutine.waitForRealsec(2f));

        coins = 0;
        playerLives = 2;
        smallBallsCount = 0;

        GameController.instance.currentLives = playerLives;
        GameController.instance.currentScore = playerScore;

        Time.timeScale = 0;

        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.FadeOut();
        }

        yield return StartCoroutine(MyCoroutine.waitForRealsec(1.25f));

        SceneManager.LoadScene(SceneManager.GetActiveScene().name) ;

        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayFadeInAnimation();
        }
    }

    public void DontWatchVideoInsteadQuit()
    {
        GameController.instance.currentScore = playerScore;


        if (GameController.instance.highScore < GameController.instance.currentScore)
        {
            GameController.instance.highScore = GameController.instance.currentScore;
            GameController.instance.Save();
        }

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene("LevelMenu");

        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayLoadingScreen();
        }
    }


}
