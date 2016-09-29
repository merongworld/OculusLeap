//
//  GameController.cs
//  OculusLeap
//
//  Created by merongworld on 09/23/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private List<Block> blocks = null;

    void Start()
    {
        blocks = new List<Block>();
        blocks.Add(new Block());
    }

    void Update()
    {

    }
}
