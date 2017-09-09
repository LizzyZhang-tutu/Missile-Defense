using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform m_Tranform;
    private Transform player_Transform;

    void Start() {
        player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_Tranform = gameObject.GetComponent<Transform>();

        }


	void Update () {
        m_Tranform.position = player_Transform.position + new Vector3(0, 50, 0);
	}
}
