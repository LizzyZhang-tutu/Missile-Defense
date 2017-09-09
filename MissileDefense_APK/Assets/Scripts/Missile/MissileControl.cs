using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControl : MonoBehaviour {

    private Transform m_Transform;
    private Transform player_Transform;
    private Vector3 normalForward = Vector3.forward;
    private GameObject explosion;


    void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        explosion = Resources.Load<GameObject>("Explode07");
    }


    void Update () {
        m_Transform.Translate(Vector3.forward * 2.1f);
        //Calculate direction.
        Vector3 dir = player_Transform.position - m_Transform.position;
        normalForward = Vector3.Lerp(normalForward, dir, Time.deltaTime);
        //Change the direction of the missile.
        m_Transform.rotation = Quaternion.LookRotation(normalForward);
	}

    /// <summary>
    /// missiles hit explosion.
    /// </summary>
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "missile")
        {
            // explosion effect.
            GameObject.Instantiate(explosion, m_Transform.position, Quaternion.identity);
            // destroy missile.
            GameObject.Destroy(gameObject);
        }
    }
}
