//
//  Block.cs
//  OculusLeap
//
//  Created by merongworld on 09/27/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : ScriptableObject
{
    public BlockType Type { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
    public GameObject GameObject { get; set; }

    public Block()
    {
        Type = BlockType.Grass;
        Position = new Vector3();
        Scale = new Vector3(1, 1, 1);
        GameObject = Instantiate(Resources.Load("Block")) as GameObject;
    }

    public Block(BlockType type)
    {
        Type = type;
        Position = new Vector3();
        Scale = new Vector3(1, 1, 1);
        GameObject = Instantiate(Resources.Load("Block")) as GameObject;
    }
}
