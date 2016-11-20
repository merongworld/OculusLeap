//
//  HUDController.cs
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

    // Use this for initialization
    void Start()
    {
        menuCanvas = GameObject.Find("MenuCanvas");
        // mainPanel.SetActive(false);
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
                break;

            case "MainCloseButton":
                menuCanvas.SetActive(false);
                break;
        }
    }
}
