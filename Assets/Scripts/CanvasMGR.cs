using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Leap;
using Leap.Unity;
using UnityEngine.UI;

public class CanvasMGR : MonoBehaviour {

    private Boolean UI_Mode;
    private int handcount;
    private string handtext;

    [Header("Hands")]
    public CapsuleHand left_hand;
    public CapsuleHand right_hand;

    [Header("Panels")]
    public GameObject block_panel;
    public GameObject geometry_panel;
    public Text mount_panel;

    // Use this for initialization
    void Start () {
        UI_Mode = false;
        block_panel.SetActive(false);
        geometry_panel.SetActive(false);
	}

    // Update is called once per frame
    void Update() {

        Vector3 upNormal = new Vector3(0, 1, 0);
        if(Vector3.Dot(UnityVectorExtension.ToVector3(left_hand.GetLeapHand().PalmNormal), upNormal) > 0 && Vector3.Dot(UnityVectorExtension.ToVector3(right_hand.GetLeapHand().PalmNormal), upNormal) > 0 && right_hand.GetLeapHand().GrabStrength > 0.9)
        {
            block_panel.SetActive(true);
        }
        else if(left_hand.GetLeapHand().GrabStrength > 0.9)
        {
            geometry_panel.SetActive(true);
        }
    }
}
