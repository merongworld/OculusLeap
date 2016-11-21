//
//  GameController.cs
//  OculusLeap
//
//  Created by merongworld on 11/21/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;

public enum MenuState
{
    None,
    Main,
    Add,
    Edit,
    Settings
}

public enum ActionState
{
    None
}

public class GameController : MonoBehaviour
{
    private MenuState menuState;
    private ActionState actionState;

    // Use this for initialization
    void Start()
    {
        menuState = MenuState.None;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public MenuState GetMenuState()
    {
        return menuState;
    }

    public ActionState GetActionState()
    {
        return actionState;
    }
}
