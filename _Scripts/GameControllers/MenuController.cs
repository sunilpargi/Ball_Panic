using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Animator settingBtnAnim;

    private bool hidden;
    private bool canTouchSettingBtn;

    [SerializeField]
    private Button musicBtn;

    [SerializeField]
    private Sprite[] musicBtnSprites;

    [SerializeField]
    private Button fbBtn;

    [SerializeField]
    private Sprite[] fbSprites;

    [SerializeField] private GameObject infoPanel;

    [SerializeField] private Image infoImage;

    [SerializeField] private Sprite[] infoSprites;
    private int infoIndex;
    void Start()
    {
        canTouchSettingBtn = true;
        hidden = true;

        if (GameController.instance.isMusicOn)
        {
            MusicController.instance.PlayBgMusic();
            musicBtn.image.sprite = musicBtnSprites[1];
        }
        else
        {
            MusicController.instance.StopBgMusic();
            musicBtn.image.sprite = musicBtnSprites[0];
        }

        infoIndex = 0;
        infoImage.sprite = infoSprites[0];
    }

    
    public void SettingBtn()
    {
        StartCoroutine(DisableSettingBtnWhilePlayingAnim());
    }

    IEnumerator DisableSettingBtnWhilePlayingAnim()
    {
        if (canTouchSettingBtn)
        {
            if (hidden)
            {
                canTouchSettingBtn = false;
                settingBtnAnim.Play("SlideIn");
                hidden = false;
                yield return StartCoroutine(MyCoroutine.waitForRealsec(1.2f));
                canTouchSettingBtn = true;
            }

            else
            {
                canTouchSettingBtn = false;
                settingBtnAnim.Play("SlideOut");
                hidden = true;
                yield return StartCoroutine(MyCoroutine.waitForRealsec(1.2f));
                canTouchSettingBtn = true;
            }
        }
    }
    
    public void MusicBtn()
    {
        if (GameController.instance.isMusicOn)
        {
            musicBtn.image.sprite = musicBtnSprites[0];
            MusicController.instance.StopBgMusic();
            GameController.instance.isMusicOn = false;
            GameController.instance.Save();
        }
        else
        {
            musicBtn.image.sprite = musicBtnSprites[1];
            MusicController.instance.PlayBgMusic();
            GameController.instance.isMusicOn = true;
            GameController.instance.Save();
        }
    }


    public void OpenInfoPanel()
    {
        infoPanel.SetActive(true);
    }

    public void CLoseInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    public void NextInfo()
    {
        infoIndex++;

        if(infoIndex == infoSprites.Length)
        {
            infoIndex = 0;
        }

        infoImage.sprite = infoSprites[infoIndex];
    }

    public void PlayBtn()
    {
        MusicController.instance.PlayClickClip();
        SceneManager.LoadScene("PlayerMenu");
    }

    public void ShopBtn()
    {
        MusicController.instance.PlayClickClip();
        SceneManager.LoadScene("ShopMenu");
    }
}
