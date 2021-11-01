using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
    public static ShopMenuController instance;

    public Text coinText, scoreText, buyArrowsText, watchVideoText;

    public Button weaponsTabBtn, specialTabBtn, earnCoinsBtn, yesBtn;
    
    public GameObject  weaponItemPanel, specialItemsPanel, earnCoinItmesPanel, coinShopPanel, buyArrowPanel;

    public AudioClip clickClip, unlockItemClip;

    private void Awake()
    {
        MakeInstance();
    }
    void Start()
    {

    }

    private void MakeInstance()
    {
       if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   public void BuyDoubleArrow()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        if (!GameController.instance.weapons[1])
        {
            if(GameController.instance.coins >= 7000)
            {
               
                buyArrowPanel.SetActive(true);
                buyArrowsText.text = "Do you Want to buy?";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyArrow(1));
            }

           else
            {
                buyArrowPanel.SetActive(true);
                buyArrowsText.text = "Dont have enough coins";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void BuyStickyArrow()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        if (!GameController.instance.weapons[2])
        {
            if (GameController.instance.coins >= 7000)
            {
             
                buyArrowPanel.SetActive(true);
                buyArrowsText.text = "Do you Want to buy?";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyArrow(2));
            }

            else
            {
                buyArrowPanel.SetActive(true);
                buyArrowsText.text = "Dont have enough coins";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void BuyDoubleStickyArrow()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        if (!GameController.instance.weapons[3])
        {
            if (GameController.instance.coins >= 7000)
            {
                buyArrowPanel.SetActive(true);
                buyArrowsText.text = "Do you Want to buy?";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyArrow(3));
            }

            else
            {
                buyArrowPanel.SetActive(true);
                buyArrowsText.text = "Dont have enough coins";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void BuyArrow(int index)
    {
        AudioSource.PlayClipAtPoint(unlockItemClip, Camera.main.transform.position);
        GameController.instance.weapons[index] = true;
        GameController.instance.coins -= 7000;
        GameController.instance.Save();

        buyArrowPanel.SetActive(false);
        coinText.text = "" + GameController.instance.coins;
    }

     void InitializeShopMenuController()
    {
        coinText.text = "" + GameController.instance.coins;
        scoreText.text = "" + GameController.instance.highScore;
    }



    public void OpenCoinShop()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        coinShopPanel.gameObject.SetActive(true);
    }

    public void CloseCoinShop()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        coinShopPanel.gameObject.SetActive(false    );
    }
   
    public void OpenWeaponItemsPanel()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        weaponItemPanel.SetActive(true);
        specialItemsPanel.SetActive(false);
        earnCoinItmesPanel.SetActive(false);
    }

    public void OpenSpecialItemsPanel()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        weaponItemPanel.SetActive(false);
        specialItemsPanel.SetActive(true);
        earnCoinItmesPanel.SetActive(false);
    }

    public void OpenEarnItemsPanel()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        weaponItemPanel.SetActive(false);
        specialItemsPanel.SetActive(false);
        earnCoinItmesPanel.SetActive(true);
    }

    public void Playgame()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        SceneManager.LoadScene("PlayerMenu");
    }

    public void GoTOMainMenu()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        SceneManager.LoadScene("MainMenu");
    }

    public void DontBuyArrows()
    {
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
        buyArrowPanel.SetActive(false);
    }
}
