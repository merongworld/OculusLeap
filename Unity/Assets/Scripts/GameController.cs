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
        AddBlock(BlockType.Grass);
    }

    void Update()
    {
        if (blocks.Count > 0)
            DeleteBlock(blocks[0]);
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

    }

    public void ScaleBlock(Block block, Vector3 scale)
    {

    }

    public void SetBlockType(Block block, BlockType type)
    {

    }
}
