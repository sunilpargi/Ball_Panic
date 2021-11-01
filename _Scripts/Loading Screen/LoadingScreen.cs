using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    [SerializeField]
    GameObject bgImage, logoImage, text, fadePanel;

    [SerializeField] private Animator fadePanelAnim ;


    void Awake()
    {
        MakeSingleton();
        Hide();
    }

    private void MakeSingleton()
    {
       if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayLoadingScreen()
    {
        StartCoroutine(ShowLoadingScreen());
    }

    public void PlayFadeInAnimation()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fadePanelAnim.Play("FadeIn");
        yield return StartCoroutine(MyCoroutine.waitForRealsec(.4f));

        if(GamePlayController.instance != null)
        {
            GamePlayController.instance.setHasLevelBegan(true);

        }
        yield return StartCoroutine(MyCoroutine.waitForRealsec(.9f));
        fadePanel.SetActive(true);
    }

    public void FadeOut()
    {
        fadePanel.SetActive(true);
        fadePanelAnim.Play("FadeOut");
    }
    IEnumerator ShowLoadingScreen()
    {
        Show();

        yield return StartCoroutine(MyCoroutine.waitForRealsec(1f));
        Hide();

        if(GamePlayController.instance != null)
        {
            GamePlayController.instance.setHasLevelBegan(true);
        }
    }

   void Show()
    {
        bgImage.SetActive(true);
        logoImage.SetActive(true);
        text.SetActive(true);
    }

    void Hide()
    {
        bgImage.SetActive(false);
        logoImage.SetActive(false);
        text.SetActive(false);
    }

   

    
}
