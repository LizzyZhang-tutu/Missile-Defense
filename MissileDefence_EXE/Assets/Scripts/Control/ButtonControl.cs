using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Button Press Control.
/// </summary>
public class ButtonControl: MonoBehaviour
{

    private WarPlane m_WarPlane;
    private GameObject left;
    private GameObject right;

    void Start()
    {
        left = GameObject.Find("Left");
        right = GameObject.Find("Right");

        m_WarPlane = GameObject.FindGameObjectWithTag("Player").GetComponent<WarPlane>();

        UIEventListener.Get(left).onPress = LeftPress;
        UIEventListener.Get(right).onPress = RightPress;
    }


    /// <summary>
    /// Left Press Button.
    /// </summary>
    private void LeftPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            //Debug.Log("Left Press");
            m_WarPlane.IsLeft = true;
        }
        else
        {
            //Debug.Log("Left Press Over");
            m_WarPlane.IsLeft = false;
        }
    }


    /// <summary>
    /// Right Press Button.
    /// </summary>
    private void RightPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            //Debug.Log("Right Press");
            m_WarPlane.IsRight = true;
        }
        else
        {
            //Debug.Log("Right Press Over");
            m_WarPlane.IsRight = false;
        }
    }
}
