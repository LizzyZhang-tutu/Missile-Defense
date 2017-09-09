using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Warplane Manager.
/// </summary>
public class WarPlaneManager : MonoBehaviour {

    private GameObject model;
    private GameObject player;

	void Start () {
        //read player's info.
        string plane = PlayerPrefs.GetString("PlayerName"); //get plane's name from playerpref.
        int speed = PlayerPrefs.GetInt("PlayerSpeed");
        int rotate = PlayerPrefs.GetInt("PlayerRotate");

        //Load model, instantiate player.
        model = Resources.Load<GameObject>(plane); 
        //instantiate a warplane.
        player = GameObject.Instantiate(model, new Vector3(0,0,-5.0f), Quaternion.identity);
        
        //Add Warplane Script, Set properties.
        WarPlane myPlane = player.AddComponent<WarPlane>();
        myPlane.Speed = speed;
        myPlane.Rotate = rotate;

        player.tag = "Player";
        player.GetComponent<Transform>().localScale = new Vector3(1.7f, 1.7f, 1.7f);
	}
	
}
