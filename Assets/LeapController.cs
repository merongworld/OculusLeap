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
    private LeapProvider provider;
    private HandState handState;
    private Hand leftHand;
    private Hand rightHand;

    // Use this for initialization
    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        handState = HandState.None;
        leftHand = null;
        rightHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        Frame frame = provider.CurrentFrame;
        List<Hand> hands = frame.Hands;
        int handCount = hands.Count;

        SetBothHandsToNull();
        switch (handCount)
        {
            case 1:
                HandleOneHand(hands[0]);
                break;

            case 2:
                HandleTwoHands(hands);
                break;

            case 0:
                handState = HandState.None;
                break;

            default: // When more than two hands are detected
                handState = HandState.Invalid;
                break;
        }

        Debug.Log(handState);
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

    private void HandleTwoHands(List<Hand> hands)
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

        // Handle when two hands are detected as the same type
        if (!AreBothHandsNotNull())
        {
            handState = HandState.Invalid;
            SetBothHandsToNull();
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

    public HandState GetHandState()
    {
        return handState;
    }

    public Hand GetLeftHand()
    {
        return leftHand;
    }

    public Hand GetRightHand()
    {
        return rightHand;
    }
}
