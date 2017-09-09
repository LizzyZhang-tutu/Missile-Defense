using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Missiles Manager.
/// </summary>
public class MissileManager : MonoBehaviour {

    private Transform m_Transform;
    //missile creating points' array.
    private Transform[] createPoints;
    //missile prefab.
    private GameObject prefab_Missile_3;


    void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        //find missile creating points.
        createPoints = GameObject.Find("CreatePoints").GetComponent<Transform>().GetComponentsInChildren<Transform>();
        //load missile model.
        prefab_Missile_3 = Resources.Load<GameObject>("Missile_3");
        //start creating missiles.
        InvokeRepeating("CreateMissile", 3, 3);
	}
	

    /// <summary>
    /// Create missiles at random points.
    /// </summary>
	private void CreateMissile() {
        int index = Random.Range(0, createPoints.Length);
        GameObject.Instantiate(prefab_Missile_3, createPoints[index].position, Quaternion.identity, m_Transform);
    }


    /// <summary>
    /// Stop creating missiles.
    /// </summary>
    public void StopCreateMissile() {
        CancelInvoke();
    }
}
