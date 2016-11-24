//
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
        if (leapController.HandState == HandState.Invalid) { return; }

        switch (gameController.ActionState)
        {
            case ActionState.None:
                HandleNoneActionState(gameController.MenuState);
                break;

            case ActionState.Attach:
                HandleAttachActionState(gameController.MenuState);
                break;

            default:
                break;
        }
    }

    private void HandleNoneActionState(MenuState menuState)
    {
        Hand rightHand = leapController.RightHand;

        if (menuState == MenuState.None)
        {
            if (rightHand == null) { return; }

            if (rightHand.GrabStrength > 0.9f)
            {
                gameController.MenuState = MenuState.Main;
            }
        }
    }

    private void HandleAttachActionState(MenuState menuState)
    {
        Hand leftHand = leapController.LeftHand;
        Hand rightHand = leapController.RightHand;

        if (menuState == MenuState.Add)
        {
            if (leapController.HandState == HandState.None) { return; }

            if (leftHand != null)
            {
                if (leftHand.GrabStrength > 0.9f)
                {
                    gameController.MenuState = MenuState.None;
                    gameController.ActionState = ActionState.None;
                }
            }

            if (rightHand != null)
            {
                string resourcePath =
                    GetResourcePathFromBlockType(gameController.SelectedBlockType);
                if (resourcePath == null) { return; }

                Object resource = Resources.Load(resourcePath, typeof(GameObject));

                if (gameController.BlockCount == 0)
                {

                }

                gameController.ActionState = ActionState.Move;
            }
        }
    }

    private string GetResourcePathFromBlockType(BlockType blockType)
    {
        switch (gameController.SelectedBlockType)
        {
            case BlockType.Grass: return "BookShelfBlock";
            case BlockType.Dirt: return "DirtBlock";
            case BlockType.Wool: return "WoolBlock";
            case BlockType.Brick: return "BrickBlock";
            case BlockType.Bookshelf: return "BookshelfBlock";
            case BlockType.Pumpkin: return "PumpkinBlock";
            case BlockType.Hay: return "HayBlock";
            case BlockType.Chest: return "ChestBlock";
            case BlockType.Tnt: return "TntBlock";
            default: return null;
        }
    }
}
