using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// warplane control.
/// </summary>
public class WarPlane : MonoBehaviour {

    private Transform m_Transform;

    private bool isLeft;//tern left.
    public bool IsLeft
    {
        get
        { return isLeft; }
        set
        { isLeft = value; }
    }

    private bool isRight;//tern right.
    public bool IsRight
    {
        get
        { return isRight; }
        set
        { isRight = value; }
    }

    //player alive or not.
    private bool isAlive = true;
    //load MissileManager script.
    private MissileManager m_MissileManager;
    //load explosion effect.
    private GameObject explosion;
    //load reward audio.
    private AudioSource rewardGet;
    //load crash audio.
    private AudioSource crash;
    //game reward.
    private int RewardNum; 

    private GameUI m_GameUI;

    private int speed;
    public int Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    private int rotate;
    public int Rotate
    {
        get
        {
            return rotate;
        }

        set
        {
            rotate = value;
        }
    }


    void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_MissileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
        explosion = Resources.Load<GameObject>("Explode07");
        rewardGet = GameObject.Find("rewardGet").GetComponent<AudioSource>();
        crash = GameObject.Find("crash").GetComponent<AudioSource>();
        m_GameUI = GameObject.Find("UI Root").GetComponent<GameUI>();
    }

    /// <summary>
    /// Control player to rotate and move WHEN ALIVE.
    /// </summary>
    void Update()
    {
        if(isAlive == true)
        {
            m_Transform.Translate(Vector3.forward * speed);

            if (isLeft)
            {
                m_Transform.Rotate(Vector3.up * -1 * rotate);
            }
            if (isRight)
            {
                m_Transform.Rotate(Vector3.up * 1 * rotate);
            }
        }
    }

    /// <summary>
    /// different effects when touching different objects.
    /// </summary>
    private void OnTriggerEnter(Collider coll)
    {
        //touch border, die.
        if(coll.tag == "border")
        {
            isAlive = false;
            crash.Play();
            m_MissileManager.StopCreateMissile();
            m_GameUI.ShowOverPanel();
        }

        //attacked by missile, die.
        if (coll.tag == "missile")
        {
            isAlive = false;
            //destroy missile.
            GameObject.Destroy(coll.gameObject);
            //hide warplane.
            gameObject.SetActive(false);
            //stop creating missiles.
            m_MissileManager.StopCreateMissile();
            //play exploding effect.
            GameObject.Instantiate(explosion, m_Transform.position, Quaternion.identity);
            //show game over panel.
            m_GameUI.ShowOverPanel();
        }

        //get a reward.
        if (coll.tag == "reward")
        {
            RewardNum++;
            //play rewardget audio.
            rewardGet.Play();
            //update score label on game UI.
            m_GameUI.UpdateScoreLabel(RewardNum);
            //destroy reward if got by player.
            GameObject.Destroy(coll.gameObject);
        }
    }
}
