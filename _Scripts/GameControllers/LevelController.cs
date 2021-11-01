using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Text scoreText, coinText;

    public bool[] levels;

    public Button[] levelButton;

    public Text[] levelText;

    public Image[] lockIcons;

    public GameObject coinShopPanel;

    public AudioClip clickClip;
    void Start()
    {
        initialization();
    }



    void initialization()
    {
        scoreText.text = "" + GameController.instance.highScore;
        coinText.text = "" + GameController.instance.coins;

        levels = GameController.instance.levels;

        for(int i = 1; i < levels.Length; i++)
        {
            if (levels[i])
            {
                lockIcons[i - 1].gameObject.SetActive(false);
            }
            else
            {
                levelButton[i - 1].interactable = false;
                levelText[i - 1].gameObject.SetActive(false);

            }
        }
    }

    public void OpenCoinPanel()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        coinShopPanel.SetActive(true);
    }

    public void CloseCoinPanel()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        coinShopPanel.SetActive(false);
    }

    public void GOTOMainMenu()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        SceneManager.LoadScene("MainMenu");
    }

    public void GOTOBackButton()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        SceneManager.LoadScene("PlayerMenu");
    }

    public void Loadlevel()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        if (GameController.instance.isMusicOn)
        {
            MusicController.instance.GameIsLoadedTurnOfMusic();
        }

        string level = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch (level)
        {
               case "Level0":               
                    GameController.instance.currentLevel = 0;
                    break;

            case "Level1":
                GameController.instance.currentLevel = 1;
                break;

            case "Level2":
                GameController.instance.currentLevel = 2;
                break;

            case "Level3":
                GameController.instance.currentLevel = 3;
                break;

            case "Level4":
                GameController.instance.currentLevel = 4;
                break;
            case "Level5":
                GameController.instance.currentLevel = 5;
                break;

        }
        LoadingScreen.instance.PlayLoadingScreen();
        GameController.instance.isGameStartedFromLevelMenu = true;
        //SceneManager.LoadScene(level);
        SceneManager.LoadScene(level);
    }
}
