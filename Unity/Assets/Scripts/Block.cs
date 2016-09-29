//
//  Block.cs
//  OculusLeap
//
//  Created by merongworld on 09/27/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class Block : ScriptableObject
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }

    public Block()
    {
        Position = new Vector3();
        Scale = new Vector3(1, 1, 1);
    }
}
