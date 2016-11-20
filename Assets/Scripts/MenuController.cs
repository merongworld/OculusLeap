//
//  MenuController.cs
//  OculusLeap
//
//  Created by merongworld on 11/21/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    private GameObject menuCanvas;
    private GameObject mainPanel;
    private GameObject addPanel;
    private GameObject editPanel;
    private GameObject settingsPanel;

    // Use this for initialization
    void Start()
    {
        menuCanvas = GameObject.Find("MenuCanvas");
        mainPanel = GameObject.Find("MainPanel");
        addPanel = GameObject.Find("AddPanel");
        editPanel = GameObject.Find("EditPanel");
        settingsPanel = GameObject.Find("SettingsPanel");

        // menuCanvas.SetActive(false);
        addPanel.SetActive(false);
        editPanel.SetActive(false);
        settingsPanel.SetActive(false);
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
                mainPanel.SetActive(false);
                addPanel.SetActive(true);
                break;

            case "MainEditButton":
                mainPanel.SetActive(false);
                editPanel.SetActive(true);
                break;

            case "MainSettingsButton":
                mainPanel.SetActive(false);
                settingsPanel.SetActive(true);
                break;

            case "MainCloseButton":
                menuCanvas.SetActive(false);
                break;
        }
    }
}
