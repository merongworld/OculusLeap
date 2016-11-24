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

    private Camera mainCamera;
    private StatePanelController statePanelController;

    private GameObject canvas;
    private GameObject mainPanel;
    private GameObject settingsPanel;

    // Use this for initialization
    void Start()
    {
        menuState = MenuState.None;

        mainCamera = Camera.main;
        statePanelController = FindObjectOfType<StatePanelController>();

        canvas = GameObject.Find("Canvas");
        mainPanel = GameObject.Find("MainPanel");
        settingsPanel = GameObject.Find("SettingsPanel");

        UpdateBackgroundColor(0.125f, 0.125f, 0.125f);

        canvas.SetActive(false);
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
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

    public Camera MainCamera
    {
        get { return mainCamera; }
    }

    private void UpdateMenuState(MenuState newMenuState)
    {
        switch (menuState)
        {
            case MenuState.Main:
                mainPanel.SetActive(false);
                break;

            case MenuState.Settings:
                settingsPanel.SetActive(false);
                break;
        }

        switch (newMenuState)
        {
            case MenuState.None:
                canvas.SetActive(false);
                break;

            case MenuState.Main:
                canvas.SetActive(true);
                statePanelController.UpdateTitleText("MAIN MENU");
                mainPanel.SetActive(true);
                break;

            case MenuState.Settings:
                canvas.SetActive(true);
                statePanelController.UpdateTitleText("SETTINGS");
                settingsPanel.SetActive(true);
                break;
        }

        menuState = newMenuState;
    }

    public void UpdateBackgroundColor(float r, float g, float b)
    {
        mainCamera.backgroundColor = new Color(r, g, b);
    }
}
