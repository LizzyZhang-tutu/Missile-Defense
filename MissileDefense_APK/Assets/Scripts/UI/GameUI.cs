using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameUI : MonoBehaviour {
    //Game and Gameover panel.
    private GameObject game_panel;
    private GameObject over_panel;

    //GamePanelLabels.
    private UILabel label_score;
    private UILabel label_time;
    //Buttons.
    private GameObject button_control;
    private GameObject button_reset;
    //Timer set.
    private int time;
    public int Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
            UpdateTimeLabel(time);
        }
    }

    //OverPanels.
    private UILabel totalScore;
    private UILabel starPlus;
    private UILabel timePlus;

    private AudioSource bg;

    void Start () {
        //find panels, labels and buttons.
        game_panel = GameObject.Find("Game_Panel");
        over_panel = GameObject.Find("Over_Panel");

        bg = GameObject.Find("bg").GetComponent<AudioSource>();
 
        label_score = GameObject.Find("StarCount").GetComponent<UILabel>();
        label_time = GameObject.Find("Time_1").GetComponent<UILabel>();
        label_time.text = "0:00"; // time set at 0:00

        totalScore = GameObject.Find("Score/Score_Num").GetComponent<UILabel>();
        starPlus = GameObject.Find("Star/StarPlus").GetComponent<UILabel>();
        timePlus = GameObject.Find("Time/TimePlus").GetComponent<UILabel>();

        button_control = GameObject.Find("ButtonControl");
        button_reset = GameObject.Find("Reset");

        //start timer.
        StartCoroutine("AddTime");

        UIEventListener.Get(button_reset).onClick = ButtonResetClick;
        label_score.text = "0"; // score set at 0.


        //Control Background Music.
        if (BgMusic.bgPlay == true)
        {
            bg.Play();
        }

        // hide game over panel at first.
        over_panel.SetActive(false); 
	}


    /// <summary>
    /// Update Score Label.
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScoreLabel(int score)
    {
        label_score.text = score.ToString();
    }


    /// <summary>
    /// Update Time_Label.
    /// </summary>
    private void UpdateTimeLabel(int time)
    {
        if(time < 10) {
            label_time.text = "0:0" + time;
        }
        else if(time<60) {
            label_time.text = "0:" + time;
        }else{
            label_time.text = time / 60 + ":" + time % 60;
        }
    }
    

    /// <summary>
    /// Show GameOver Panel.
    /// </summary>
    public void ShowOverPanel()
    {
        button_control.SetActive(false);
        over_panel.SetActive(true);
        StopAddTime();
        SetOverPanelInfo();
    }


    /// <summary>
    /// Back to startUI.
    /// </summary>
    /// <param name="go"></param>
    private void ButtonResetClick(GameObject go)
    {
        SceneManager.LoadScene("StartUI");
    }


    /// <summary>
    /// Set Timer.
    /// </summary>
    /// <returns></returns>
    IEnumerator AddTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            Time++;
        }
    }


    /// <summary>
    /// Stop Timer.
    /// </summary>
    private void StopAddTime()
    {
        StopCoroutine("AddTime");
    }
    

    /// <summary>
    /// Set Values for Over Panel.
    /// </summary>
    private void SetOverPanelInfo()
    {
        int temp = int.Parse(label_score.text);
        starPlus.text = "+" + temp  * 10; //star plus UI.
        timePlus.text = "+" + time.ToString(); // time plus UI.
        int total = temp * 10 + time;
        totalScore.text = total.ToString(); // update total score UI.

        //store BestScore.
        PlayerPrefs.SetInt("BestScore", total);
        //store goldNum.
        PlayerPrefs.SetInt("GoldScore", temp * 10);
    }
}
