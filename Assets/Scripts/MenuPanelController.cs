//
//  MenuPanelController.cs
//  OculusLeap
//
//  Created by merongworld on 11/21/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;

public enum PanelState
{
    None,
    Main,
    Add,
    Edit,
    Settings
}

public class MenuPanelController : MonoBehaviour
{
    private GameController gameController;
    private PanelState panelState;

    private GameObject addPanel;
    private GameObject editPanel;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        panelState = PanelState.None;

        addPanel = GameObject.Find("AddPanel");
        editPanel = GameObject.Find("EditPanel");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMainButtonClick(GameObject button)
    {
        switch (button.name)
        {
            case "MainAddButton":
                break;

            case "MainEditButton":
                break;

            case "MainSettingsButton":
                gameController.MenuState = MenuState.Settings;
                break;

            case "MainCloseButton":
                gameController.MenuState = MenuState.None;
                break;
        }
    }
}
