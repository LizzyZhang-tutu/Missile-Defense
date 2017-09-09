using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reward Item Control.
/// </summary>
public class Reward : MonoBehaviour {

    private Transform m_Transform;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
	}
	
	void Update () {
        m_Transform.Rotate(Vector3.left * 6);
	}

    void OnDestroy() {
        SendMessageUpwards("RewardCountDown"); // Send message to reward parent object.
    }
}
