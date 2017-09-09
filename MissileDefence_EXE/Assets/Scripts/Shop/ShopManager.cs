using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    private ShopData shopData;
    //Xml shopitem path;
        //private string xmlPath = "Assets/Data/ShopData.xml";
    private string xmlPath = Application.dataPath + "/Data/ShopData.xml";
    //Xml saving path;
        //private string savePath ="Assets/Data/SaveData.xml";
    private string savePath =Application.dataPath + "/Data/SaveData.xml";

    private GameObject ui_ShopItem;
    private StartUI m_startUI;

    //Two Buttons.
    private GameObject leftButton;
    private GameObject rightButton;

    //A list of all shopUI.
    private List<GameObject> shopUI = new List<GameObject>();
    //Showing shopUI's index.
    private int index = 0;

    //screen UI.
    private UILabel starNum;
    private UILabel scoreNum;

    //Audios.
    private AudioSource click;
    private AudioSource bought;
    private AudioSource lackMoney;

    void Start () {
        //instantiate shopdata object.
        shopData = new ShopData();
        //load xml file.
        shopData.ReadXmlByPath(xmlPath);
        //load shopItem and startUI.
        ui_ShopItem = Resources.Load<GameObject>("UI/ShopItem");
        m_startUI = GameObject.Find("UI Root").GetComponent<StartUI>();
        //load goldAndScore data file.
        shopData.ReadItemData(savePath);
        //load audio sources.
        click = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        bought = GameObject.Find("UI Root").GetComponent<AudioSource>();
        lackMoney = GameObject.Find("Camera").GetComponent<AudioSource>();


        //Button Event Binding.
        leftButton = GameObject.Find("LeftButton").gameObject;
        rightButton = GameObject.Find("RightButton").gameObject;
        UIEventListener.Get(leftButton).onClick = LeftButtonClick;
        UIEventListener.Get(rightButton).onClick = RightButtonClick;

        //Update all datas of UI 
        starNum = GameObject.Find("Star/StarNum").GetComponent<UILabel>();
        scoreNum = GameObject.Find("BestScore/Score_Label").GetComponent<UILabel>();

        ///BEST SCORE STORING.
        //Read Bestscore from PlayerPrefs.
        int tempBestScore = PlayerPrefs.GetInt("BestScore", 0);
        if(tempBestScore > shopData.bestScore)
        {
            //UPDATE UI.
            UpdateUIBestScore(tempBestScore);
            //UPDATE XML file.
            shopData.UpdateXMLData(savePath, "BestScore", tempBestScore.ToString());
            //RESET PlayerPrefs.
            PlayerPrefs.SetInt("BestScore", 0);
        }else{
            //UPDATE UI.
            UpdateUIBestScore(shopData.bestScore);
        }

        ///GOLD SCORE STORING.
        // Read GoldScore from PlayerPrefs.
        int tempGoldScore = PlayerPrefs.GetInt("GoldScore", 0);
        if (tempGoldScore > 0)
        {
            int totalGold = shopData.goldCount + tempGoldScore;
            //UPDATE UI.
            UpdateUIGoldCount(totalGold);
            //UPDATE XML file.
            shopData.UpdateXMLData(savePath, "GoldCount", totalGold.ToString());
            PlayerPrefs.SetInt("GoldScore", 0);
        }
        else
        {
            //UPDATE UI.
            UpdateUIGoldCount(shopData.goldCount);
        }
        //Set Default item.
        SetPlayerInfo(shopData.shopList[0]);
        //Create all shopUI.
        CreateAllShopUI();
	}
	

    /// <summary>
    /// Create all items in shop.
    /// </summary>
    private void CreateAllShopUI()
    {
        for (int i = 0; i < shopData.shopList.Count; i++)
        {
            //instantiate a single shopUI.
            GameObject item = NGUITools.AddChild(gameObject, ui_ShopItem);
            //Load according warplane models.
            GameObject ship = Resources.Load<GameObject>(shopData.shopList[i].Model);
            //set values for UI elements.
            item.GetComponent<ShopItemUI>().SetUIValue
                (shopData.shopList[i].Speed, shopData.shopList[i].Rotate, 
                shopData.shopList[i].Price, ship, shopData.shopState[i], shopData.shopList[i].Id);
            //Put created UI into a list.
            shopUI.Add(item);
        }
        ShopUIHideAndShow(index);
    }


    /// <summary>
    /// LeftButton Click.
    /// </summary>
    private void LeftButtonClick(GameObject go)
    {
        if (index > 0)
        {
            index--;
            int temp = shopData.shopState[index];
            m_startUI.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index]);
            ShopUIHideAndShow(index);
            click.Play();
        }
    }


    /// <summary>
    /// RightButton Click.
    /// </summary>
    private void RightButtonClick(GameObject go)
    {
        if (index < shopUI.Count - 1)
        {
            index++;
            int temp = shopData.shopState[index];
            m_startUI.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index]);
            ShopUIHideAndShow(index);
            click.Play();
        }
    }


    /// <summary>
    /// ShopUI hides and shows.
    /// </summary>
    private void ShopUIHideAndShow(int index)
    {
        for (int i = 0; i < shopUI.Count; i++)
        {
            shopUI[i].SetActive(false);
        }
        shopUI[index].SetActive(true);
    }


    /// <summary>
    /// Calculate shopItem's price.
    /// </summary>
    private void CalcItemPrice(ShopItemUI item)
    {
        if(shopData.goldCount >= item.ItemPrice)
        {
            Debug.Log("success purchase!");
            item.Bought();                           //hide BuyButton.
            shopData.goldCount -= item.ItemPrice;    // minus goldCount.
            UpdateUI();                              //update UI.
            shopData.UpdateXMLData(savePath, "GoldCount", shopData.goldCount.ToString()); //update Xml goldcount.
            shopData.UpdateXMLData(savePath, "ID" + item.ItemId, "1"); //if bought change status to 1.
            bought.Play(); //Play bought audio.
        }else{
            //Debug.Log("you don't have enough money!");
            lackMoney.Play(); //Play purchase fail audio.
        }
    }


    /// <summary>
    /// Update star and score UI.
    /// </summary>
    private void UpdateUI()
    {
        starNum.text = shopData.goldCount.ToString();
        scoreNum.text = shopData.bestScore.ToString();
    }


    /// <summary>
    /// update best score UI.
    /// </summary>
    private void UpdateUIBestScore(int bestScore)
    {
        scoreNum.text = bestScore.ToString();
    }


    /// <summary>
    /// update gold quantity UI.
    /// </summary>
    private void UpdateUIGoldCount(int gold)
    {
        starNum.text = gold.ToString();
    }
    

    /// <summary>
    /// Set player's information.
    /// </summary>
    private void SetPlayerInfo(ShopItem item)
    {
        PlayerPrefs.SetString("PlayerName", item.Model);
        PlayerPrefs.SetInt("PlayerSpeed", int.Parse(item.Speed));
        PlayerPrefs.SetInt("PlayerRotate", int.Parse(item.Rotate));
    }
}
