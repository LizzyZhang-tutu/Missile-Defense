using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reward Manager.
/// </summary>
public class RewardManager : MonoBehaviour {

    private Transform m_Transform;
    //reward prefab.
    private GameObject prefab_reward; 

    private int rewardCount = 0; //number of rewards in game.
    private int rewardMaxCount = 5; //max number of rewards in game.


	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        prefab_reward = Resources.Load<GameObject>("reward");
        //start creating reward objects.
        InvokeRepeating("CreateReward", 3, 3);
	}

    /// <summary>
    /// Create Rewards.
    /// </summary>
    private void CreateReward()
    {
        //no more than 5 rewards at one time.
        if (rewardCount < rewardMaxCount)
        {
            Vector3 pos = new Vector3(Random.Range(-730, 740), 0, Random.Range(-800, 580));
            GameObject.Instantiate(prefab_reward, pos, Quaternion.identity, m_Transform);
            rewardCount++;
            
        }
    }

    /// <summary>
    /// Stop Creating Rewards.
    /// </summary>
    public void StopCreateReward()
    {
        CancelInvoke();
    }

    /// <summary>
    /// calculate reward item's quantity in game.
    /// </summary>
    public void RewardCountDown()
    {
        rewardCount--;
    }
}
