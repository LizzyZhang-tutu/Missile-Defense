using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemUI : MonoBehaviour {

    private Transform m_Transform;

    private UILabel ui_Speed;
    private UILabel ui_Rotate;
    private UILabel ui_Price;

    private StartUI m_startUI;
    private GameObject shipParent; //ship model's parent object.

    private GameObject buyButton; 
    private GameObject itemState; //show buybottom or not.

    private int itemPrice;
    public int ItemPrice
    {
        get
        {
            return itemPrice;
        }

        set
        {
            itemPrice = value;
        }
    }

    private int itemId;
    public int ItemId
    {
        get
        {
            return itemId;
        }

        set
        {
            itemId = value;
        }
    }

    private AudioSource modelGet;

    void Awake () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_startUI = GameObject.Find("UI Root").GetComponent<StartUI>();

        //find properties' UI.
        ui_Speed = m_Transform.Find("Speed/Speed_Num").GetComponent<UILabel>();
        ui_Rotate = m_Transform.Find("Rotate/Rotate_Num").GetComponent<UILabel>();
        ui_Price = m_Transform.Find("BuyButton/Price").GetComponent<UILabel>();

        shipParent = m_Transform.Find("Model").gameObject;
        //find buy button.
        buyButton = m_Transform.Find("BuyButton/bg").gameObject;
        itemState = m_Transform.Find("BuyButton").gameObject;
        //audio load.
        modelGet = GameObject.Find("Shop").GetComponent<AudioSource>();
        UIEventListener.Get(buyButton).onClick = BuyButtonClick;
    }


    /// <summary>
    /// Set items' UI values.
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="rotate"></param>
    /// <param name="price"></param>
    /// <param name="model"></param>
    /// <param name="state"></param>
    /// <param name="id"></param>
    public void SetUIValue(string speed, string rotate, string price, GameObject model, int state, string id)
    {
        //set shopItems' UI values.
        ui_Speed.text = speed;
        ui_Rotate.text = rotate;
        ui_Price.text = price;

        itemPrice = int.Parse(price);
        itemId = int.Parse(id);

        //if item bought, hide BuyButton.
        if(state == 1)
        {
            itemState.SetActive(false);
        }

        m_Transform.localPosition = new Vector3(0, -134, 0);
        m_Transform.localScale = new Vector3(0.67f, 0.67f, 0.67f);
        //instantiate a ship and set details.
        GameObject plane = NGUITools.AddChild(shipParent, model);
        plane.layer = 8; //set model's layer.
        Transform plane_Transform = plane.GetComponent<Transform>();
        plane_Transform.Find("Mesh").gameObject.layer = 8; //set model's child layer.

        //set model's position and scale.
        plane_Transform.localScale = new Vector3(15,15,15);
        plane_Transform.localPosition = new Vector3(0, -10, 0); //scale.
        plane_Transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0)); //position.
    }


    /// <summary>
    /// Buy Button Clicked.
    /// </summary>
    /// <param name="go"></param>
    private void BuyButtonClick(GameObject go)
    {
        //Debug.Log("BUY");
        SendMessageUpwards("CalcItemPrice", this);
    }


    /// <summary>
    /// Item Bought.
    /// </summary>
    public void Bought()
    {
        itemState.SetActive(false);
        modelGet.Play();
        m_startUI.button_play.SetActive(true);
    }
}
