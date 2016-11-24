//
//  StatePanelController.cs
//  OculusLeap
//
//  Created by merongworld on 11/17/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap;

public enum HandIconState
{
    None,
    Valid,
    Invalid
}

// To keep track of current state
public struct CurrentState
{
    public DeviceState device;
    public HandState hand;
    public HandIconState leftHandIcon;
    public HandIconState rightHandIcon;
}

public class StatePanelController : MonoBehaviour
{
    private CurrentState currentState;
    private LeapController leapController;

    private GameObject leapMotionIcon;
    private Sprite leapMotionBlackSprite;
    private Sprite leapMotionGreenSprite;

    private GameObject leftHandIcon;
    private Sprite leftHandGraySprite;
    private Sprite leftHandRedSprite;
    private Sprite leftHandGreenSprite;

    private GameObject rightHandIcon;
    private Sprite rightHandGraySprite;
    private Sprite rightHandRedSprite;
    private Sprite rightHandGreenSprite;

    private Text titleText;
    private Text timeText;

    // Use this for initialization
    void Start()
    {
        currentState.device = DeviceState.Disconnected;
        currentState.hand = HandState.None;
        currentState.leftHandIcon = HandIconState.None;
        currentState.rightHandIcon = HandIconState.None;

        leapController = FindObjectOfType<LeapController>() as LeapController;

        leapMotionIcon = GameObject.Find("LeapMotionIcon");
        leapMotionBlackSprite = Resources.Load<Sprite>("leap_motion_black");
        leapMotionGreenSprite = Resources.Load<Sprite>("leap_motion_green");

        leftHandIcon = GameObject.Find("LeftHandIcon");
        leftHandIcon.SetActive(false);

        rightHandIcon = GameObject.Find("RightHandIcon");
        rightHandIcon.SetActive(false);

        leftHandGraySprite = Resources.Load<Sprite>("left_hand_gray");
        leftHandRedSprite = Resources.Load<Sprite>("left_hand_red");
        leftHandGreenSprite = Resources.Load<Sprite>("left_hand_green");

        rightHandGraySprite = Resources.Load<Sprite>("right_hand_gray");
        rightHandRedSprite = Resources.Load<Sprite>("right_hand_red");
        rightHandGreenSprite = Resources.Load<Sprite>("right_hand_green");

        titleText = GameObject.Find("TitleText").GetComponent<Text>();
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLeapMotionIcon();
        UpdateHandIcons();
        SetHandIconsActive(currentState.device);
        UpdateTime();
    }

    void UpdateLeapMotionIcon()
    {
        DeviceState deviceState = leapController.DeviceState;
        if (deviceState == currentState.device) { return; }

        currentState.device = deviceState;
        UnityEngine.UI.Image image =
            leapMotionIcon.GetComponent<UnityEngine.UI.Image>();

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

    void UpdateHandIcons()
    {
        HandState handState = leapController.HandState;
        Hand leftHand = leapController.LeftHand;
        Hand rightHand = leapController.RightHand;

        UnityEngine.UI.Image leftHandImage =
            leftHandIcon.GetComponent<UnityEngine.UI.Image>();

        UnityEngine.UI.Image rightHandImage =
            rightHandIcon.GetComponent<UnityEngine.UI.Image>();

        HandIconState leftHandIconState = GetHandIconState(
            currentState.device, handState, leftHand);

        HandIconState rightHandIconState = GetHandIconState(
            currentState.device, handState, rightHand);

        if (leftHandIconState != currentState.leftHandIcon)
        {
            currentState.leftHandIcon = leftHandIconState;
            UnityEngine.UI.Image image =
                leftHandIcon.GetComponent<UnityEngine.UI.Image>();

            switch (currentState.leftHandIcon)
            {
                case HandIconState.None:
                    image.sprite = leftHandGraySprite;
                    break;

                case HandIconState.Valid:
                    image.sprite = leftHandGreenSprite;
                    break;

                case HandIconState.Invalid:
                    image.sprite = leftHandRedSprite;
                    break;
            }
        }
        else if (rightHandIconState != currentState.rightHandIcon)
        {
            currentState.rightHandIcon = rightHandIconState;
            UnityEngine.UI.Image image =
                rightHandIcon.GetComponent<UnityEngine.UI.Image>();

            switch (currentState.rightHandIcon)
            {
                case HandIconState.None:
                    image.sprite = rightHandGraySprite;
                    break;

                case HandIconState.Valid:
                    image.sprite = rightHandGreenSprite;
                    break;

                case HandIconState.Invalid:
                    image.sprite = rightHandRedSprite;
                    break;
            }
        }
    }

    void SetHandIconsActive(DeviceState deviceState)
    {
        switch (deviceState)
        {
            case DeviceState.Disconnected:
                leftHandIcon.SetActive(false);
                rightHandIcon.SetActive(false);
                break;

            case DeviceState.Connected:
                leftHandIcon.SetActive(true);
                rightHandIcon.SetActive(true);
                break;
        }
    }

    HandIconState GetHandIconState(
        DeviceState deviceState, HandState handState, Hand hand)
    {
        if (deviceState == DeviceState.Disconnected)
        {
            return HandIconState.None;
        }

        switch (handState)
        {
            case HandState.None:
                return HandIconState.None;

            case HandState.Left:
                if (hand == null) { return HandIconState.None; }
                else if (hand.IsLeft) { return HandIconState.Valid; }
                break;

            case HandState.Right:
                if (hand == null) { return HandIconState.None; }
                else if (!hand.IsLeft) { return HandIconState.Valid; }
                break;

            case HandState.Both:
                return HandIconState.Valid;

            case HandState.Invalid:
                if (hand != null) { return HandIconState.Invalid; }
                return HandIconState.None;
        }

        return HandIconState.Invalid;
    }

    public void UpdateTitleText(string text)
    {
        titleText.text = text;
    }

    public void UpdateTime()
    {

    }
}
