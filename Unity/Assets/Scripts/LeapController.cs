//
//  LeapController.cs
//  OculusLeap
//
//  Created by merongworld on 11/17/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;

public enum DeviceState
{
    Disconnected,
    Connected
}

public enum HandState
{
    None,
    Left,
    Right,
    Both,
    Invalid
};

public class LeapController : MonoBehaviour
{
    private LeapServiceProvider provider;
    private Controller controller;

    private DeviceState deviceState;
    private HandState handState;
    private Hand leftHand;
    private Hand rightHand;

    // Use this for initialization
    void Start()
    {
        provider = FindObjectOfType<LeapServiceProvider>() as LeapServiceProvider;
        controller = provider.GetLeapController();

        deviceState = DeviceState.Disconnected;
        handState = HandState.None;
        leftHand = null;
        rightHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null) { return; }

        SetBothHandsToNull();
        UpdateDeviceState();

        if (deviceState == DeviceState.Disconnected)
        {
            handState = HandState.None;
        }
        else
        {
            UpdateHandState();
        }
    }

    private void UpdateDeviceState()
    {
        if (!controller.IsConnected)
        {
            deviceState = DeviceState.Disconnected;
            return;
        }

        deviceState = DeviceState.Connected;
    }

    private void UpdateHandState()
    {
        Frame frame = provider.CurrentFrame;
        List<Hand> hands = frame.Hands;
        int handCount = hands.Count;

        switch (handCount)
        {
            case 1:
                HandleOneHand(hands[0]);
                break;

            case 0:
                handState = HandState.None;
                break;

            default:
                HandleTwoOrMoreHands(hands);
                break;
        }
    }

    private void HandleOneHand(Hand hand)
    {
        switch (hand.IsLeft)
        {
            case true:
                handState = HandState.Left;
                leftHand = hand;
                break;

            default:
                handState = HandState.Right;
                rightHand = hand;
                break;
        }
    }

    private void HandleTwoOrMoreHands(List<Hand> hands)
    {
        foreach (Hand hand in hands)
        {
            switch (hand.IsLeft)
            {
                case true:
                    leftHand = hand;
                    break;

                default:
                    rightHand = hand;
                    break;
            }
        }

        if (!AreBothHandsNotNull() || hands.Count > 2)
        {
            handState = HandState.Invalid;
            return;
        }

        handState = HandState.Both;
    }

    private void SetBothHandsToNull()
    {
        leftHand = null;
        rightHand = null;
    }

    private bool AreBothHandsNotNull()
    {
        if (leftHand != null && rightHand != null)
            return true;

        return false;
    }

    public DeviceState DeviceState
    {
        get { return deviceState; }
    }

    public HandState HandState
    {
        get { return handState; }
    }

    public Hand LeftHand
    {
        get { return leftHand; }
    }

    public Hand RightHand
    {
        get { return rightHand; }
    }
}
