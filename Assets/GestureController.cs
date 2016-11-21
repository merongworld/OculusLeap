//
//  GestureController.cs
//  OculusLeap
//
//  Created by merongworld on 11/21/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class GestureController : MonoBehaviour
{
    GameController gameController;
    LeapController leapController;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        leapController = FindObjectOfType<LeapController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
