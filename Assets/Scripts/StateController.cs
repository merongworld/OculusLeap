//
//  StateController.cs
//  OculusLeap
//
//  Created by merongworld on 11/17/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// To keep track of current state
public struct State
{
    public DeviceState deviceState;
}

public class StateController : MonoBehaviour
{
    private State current;

    private LeapController leapController;
    private GameObject leapMotionIcon;

    private Sprite leapMotionBlackSprite;
    private Sprite leapMotionGreenSprite;

    // Use this for initialization
    void Start()
    {
        current.deviceState = DeviceState.Disconnected;

        leapController = FindObjectOfType<LeapController>() as LeapController;
        leapMotionIcon = GameObject.Find("LeapMotionIcon");

        leapMotionBlackSprite = Resources.Load<Sprite>("leap_motion_black");
        leapMotionGreenSprite = Resources.Load<Sprite>("leap_motion_green");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLeapMotionIcon();
    }

    void UpdateLeapMotionIcon()
    {
        DeviceState deviceState = leapController.DeviceState;
        if (deviceState == current.deviceState) { return; }

        current.deviceState = deviceState;
        Image image = leapMotionIcon.GetComponent<Image>();

        switch (deviceState)
        {
            case DeviceState.Disconnected:
                image.sprite = leapMotionBlackSprite;
                break;

            case DeviceState.Connected:
                image.sprite = leapMotionGreenSprite;
                break;
        }
    }
}
