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
    }

    void Update()
    {

    }

    public void AddBlock(BlockType type)
    {
        Block block = new Block(type);
        blocks.Add(block);
    }

    public void DeleteBlock(Block block)
    {
        if (block == null) return;
        else if (blocks.Contains(block)) blocks.Remove(block);

        Destroy(block);
    }

    public void MoveBlock(Block block, Vector3 position)
    {
        block.Position = position;
    }

    public void ScaleBlock(Block block, Vector3 scale)
    {
        block.Scale = scale;
    }

    public void SetBlockType(Block block, BlockType type)
    {
        block.SetType(type);
    }
}
