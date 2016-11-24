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
    None,
    UI,
    Attach
}

public class GameController : MonoBehaviour
{
    private MenuState menuState;
    private ActionState actionState;

    private bool didMenuChange = false;
    private float elapsedTime = 0.0f;

    private Camera mainCamera;
    private StatePanelController statePanelController;

    private GameObject leapEventSystem;
    private GameObject canvas;
    private GameObject mainPanel;
    private GameObject settingsPanel;
    private GameObject addPanel;
    private GameObject editPanel;

    // Use this for initialization
    void Start()
    {
        menuState = MenuState.None;

        mainCamera = Camera.main;
        statePanelController = FindObjectOfType<StatePanelController>();

        leapEventSystem = GameObject.Find("LeapEventSystem");
        canvas = GameObject.Find("Canvas");
        mainPanel = GameObject.Find("MainPanel");
        settingsPanel = GameObject.Find("SettingsPanel");
        addPanel = GameObject.Find("AddPanel");
        editPanel = GameObject.Find("EditPanel");

        UpdateBackgroundColor(0.125f, 0.125f, 0.125f);

        canvas.SetActive(false);
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        addPanel.SetActive(false);
        editPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (actionState)
        {
            case ActionState.None:
                break;
        }

        if (didMenuChange)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 0.15f)
            {
                didMenuChange = false;
                elapsedTime = 0.0f;
                leapEventSystem.SetActive(true);
            }
        }
    }

    public MenuState MenuState
    {
        get { return menuState; }
        set
        {
            if (menuState != value) { UpdateMenuState(value); }
        }
    }

    public ActionState ActionState
    {
        get { return actionState; }
        set
        {
            if (actionState != value) { actionState = value; }
        }
    }

    public Camera MainCamera
    {
        get { return mainCamera; }
    }

    private void UpdateMenuState(MenuState newMenuState)
    {
        if (newMenuState == menuState) { return; }

        switch (menuState)
        {
            case MenuState.Main:
                mainPanel.SetActive(false);
                break;

            case MenuState.Settings:
                settingsPanel.SetActive(false);
                break;

            case MenuState.Add:
                addPanel.SetActive(false);
                break;

            case MenuState.Edit:
                break;
        }

        if (newMenuState != MenuState.None)
        {
            leapEventSystem.SetActive(false);
            didMenuChange = true;
        }

        switch (newMenuState)
        {
            case MenuState.None:
                canvas.SetActive(false);
                ActionState = ActionState.None;
                break;

            case MenuState.Main:
                canvas.SetActive(true);
                statePanelController.UpdateTitleText("MAIN MENU");
                mainPanel.SetActive(true);
                ActionState = ActionState.UI;
                break;

            case MenuState.Settings:
                canvas.SetActive(true);
                statePanelController.UpdateTitleText("SETTINGS");
                settingsPanel.SetActive(true);
                ActionState = ActionState.UI;
                break;

            case MenuState.Add:
                canvas.SetActive(true);
                statePanelController.UpdateTitleText("ADD BLOCK");
                addPanel.SetActive(true);
                ActionState = ActionState.UI;
                break;

            case MenuState.Edit:
                statePanelController.UpdateTitleText("EDIT BLOCK");
                break;
        }

        menuState = newMenuState;
    }

    public void UpdateBackgroundColor(float r, float g, float b)
    {
        mainCamera.backgroundColor = new Color(r, g, b);
    }
}
