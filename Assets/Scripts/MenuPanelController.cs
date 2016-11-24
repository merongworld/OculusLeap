//
//  MenuPanelController.cs
//  OculusLeap
//
//  Created by merongworld on 11/21/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;
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

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        panelState = PanelState.None;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMainButtonClick(GameObject mainButton)
    {
        if (mainButton == null) { return; }

        switch (mainButton.name)
        {
            case "MainAddButton":
                gameController.MenuState = MenuState.Add;
                break;

            case "MainEditButton":
                gameController.MenuState = MenuState.Edit;
                break;

            case "MainSettingsButton":
                gameController.MenuState = MenuState.Settings;
                break;

            case "MainCloseButton":
                gameController.MenuState = MenuState.None;
                break;
        }
    }

    public void OnSettingsButtonClick(GameObject settingsButton)
    {
        if (settingsButton == null) { return; }

        switch (settingsButton.name)
        {
            case "SettingsBackButton":
                gameController.MenuState = MenuState.Main;
                break;

            case "SettingsCloseButton":
                gameController.MenuState = MenuState.None;
                break;
        }
    }

    public void OnSettingsSliderValueChanged(GameObject settingsSlider)
    {
        if (settingsSlider == null) { return; }

        float value;
        Color color;
        Slider slider = settingsSlider.GetComponent<Slider>();

        switch (settingsSlider.name)
        {
            case "SettingsRedSlider":
                value = slider.value;
                color = gameController.MainCamera.backgroundColor;
                gameController.UpdateBackgroundColor(value, color.g, color.b);
                break;

            case "SettingsGreenSlider":
                value = slider.value;
                color = gameController.MainCamera.backgroundColor;
                gameController.UpdateBackgroundColor(color.r, value, color.b);
                break;

            case "SettingsBlueSlider":
                value = slider.value;
                color = gameController.MainCamera.backgroundColor;
                gameController.UpdateBackgroundColor(color.r, color.g, value);
                break;
        }
    }

    public void OnAddButtonClick(GameObject addButton)
    {
        if (addButton == null) { return; }

        switch (addButton.name)
        {
            case "AddBackButton":
                gameController.MenuState = MenuState.Main;
                break;

            case "AddCloseButton":
                gameController.MenuState = MenuState.None;
                break;
        }
    }

    public void OnAddBlockButtonClick(GameObject addBlockButton)
    {
        if (addBlockButton == null) { return; }

        gameController.ActionState = ActionState.Attach;
        switch (addBlockButton.name)
        {
            case "AddGrassButton":
                break;

            case "AddDirtButton":
                break;

            case "AddWoolButton":
                break;

            case "AddBrickButton":
                break;

            case "AddBookshelfButton":
                break;

            case "AddPumpkinButton":
                break;

            case "AddHayButton":
                break;

            case "AddChestButton":
                break;

            case "AddTntButton":
                break;
        }
    }
}
