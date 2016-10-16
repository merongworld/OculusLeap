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
    private GameObject sphere = null;

    void Start()
    {
        blocks = new List<Block>();
        sphere = GameObject.Find("Sphere");

        AddBlock(BlockType.Grass);
    }

    void Update()
    {
        if (sphere.activeSelf)
        {
            Transform camera = Camera.main.transform;
            Debug.DrawRay(camera.position, camera.rotation * Vector3.forward);
        }
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
    block.Move(position);
}

public void ScaleBlock(Block block, Vector3 scale)
{
    block.Scale(scale);
}

public void SetBlockType(Block block, BlockType type)
{
    block.Type = type;
}
}
