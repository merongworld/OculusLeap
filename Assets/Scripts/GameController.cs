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

    private GameObject canvas;
    private GameObject mainPanel;

    // Use this for initialization
    void Start()
    {
        menuState = MenuState.None;
        canvas = GameObject.Find("Canvas");
        mainPanel = GameObject.Find("MainPanel");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public MenuState MenuState
    {
        get { return menuState; }
        set
        {
            if (menuState != value)
            {
                UpdateMenuState(value);
            }
        }
    }

    public ActionState GetActionState()
    {
        return actionState;
    }

    private void UpdateMenuState(MenuState newMenuState)
    {
        // switch to new menu state
        switch (newMenuState)
        {
            case MenuState.None:
                canvas.SetActive(false);
                break;

            case MenuState.Main:
                canvas.SetActive(true);
                mainPanel.SetActive(true);
                break;
        }

        menuState = newMenuState;
    }
}
