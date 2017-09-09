using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Start Scene UI.
/// </summary>
public class StartUI : MonoBehaviour {

    private GameObject start_panel;
    private GameObject setting_panel;

    private GameObject button_setting;
    private GameObject button_close;
    public GameObject button_play;
    private GameObject button_bg;

    private AudioSource click;
    private AudioSource bgmusic;

    void Start () { 
        //FIND START AND SETTING PANEL.
        start_panel = GameObject.Find("Start_Panel");
        setting_panel = GameObject.Find("Setting_Panel");

        //FIND BackGround Music.
        bgmusic = GameObject.Find("bg").GetComponent<AudioSource>();

        //FIND BUTTONS.
        button_setting = GameObject.Find("Setting");
        button_close = GameObject.Find("Close");
        button_play = GameObject.Find("Play");

        //AUDIO SOURCES.
        click = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        UIEventListener.Get(button_setting).onClick = SettingButtonClick;
        UIEventListener.Get(button_close).onClick = CloseButtonClick;
        UIEventListener.Get(button_play).onClick = PlayButtonClick;

        //play background music at first.
        if(BgMusic.bgPlay == true)
        {
            bgmusic.Play();
        }


        //hide setting panel at first.
        setting_panel.SetActive(false); 
    }


    /// <summary>
    /// Open setting panel.
    /// </summary>
    private void SettingButtonClick(GameObject go)
    {
        if(setting_panel.activeSelf == false)
        {
            setting_panel.SetActive(true);
            click.Play();
            button_bg = GameObject.Find("Sound");
            UIEventListener.Get(button_bg).onClick = BgMusicClick;
        }
    }

    /// <summary>
    /// Play or Stop Background Music.
    /// </summary>
    private void BgMusicClick(GameObject go)
    {
        if (BgMusic.bgPlay == false)
        {
            bgmusic.Play();
            BgMusic.bgPlay = true;
        }
        else
        {
            bgmusic.Stop();
            BgMusic.bgPlay = false;
        }
    }
    /// <summary>
    /// Close setting panel.
    /// </summary>
    private void CloseButtonClick(GameObject go)
    {
        setting_panel.SetActive(false);
        click.Play();
    }


    /// <summary>
    /// Play button clicked change to start scene.
    /// </summary>
    private void PlayButtonClick(GameObject go)
    {
        SceneManager.LoadScene("GameUI");
        click.Play();
    }


    /// <summary>
    /// Show or hide shopitem's buy botton
    /// </summary>
    public void SetPlayButtonState(int state)
    {
        if(state == 0)
        {
            button_play.SetActive(false);
        }
        else if(state == 1)
        {
            button_play.SetActive(true);
        }
    }
}
