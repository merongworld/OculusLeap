﻿//
//  InputController.cs
//  OculusLeap
//
//  Created by merongworld on 11/21/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;
using Leap;

public class InputController : MonoBehaviour
{
    private GameController gameController;
    private LeapController leapController;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        leapController = FindObjectOfType<LeapController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameController.MenuState)
        {
            case MenuState.None:
                HandleNoneMenuState();
                break;

            case MenuState.Main:
                // Do nothing
                break;
        }
    }

    private void HandleNoneMenuState()
    {
        switch (gameController.ActionState)
        {
            case ActionState.None:
                HandleNoneActionState();
                break;
        }
    }

    private void HandleNoneActionState()
    {
        Hand rightHand = leapController.RightHand;
        if (rightHand == null) { return; }

        if (rightHand.GrabStrength > 0.9)
        {
            gameController.MenuState = MenuState.Main;
        }
    }
}
