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
    private Block selectedBlock = null;
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

            Ray ray = new Ray(camera.position, Vector3.Normalize(camera.rotation * Vector3.forward));
            RaycastHit hit;
            GameObject hitObject;

            if (Physics.Raycast(ray, out hit))
            {
                hitObject = hit.collider.gameObject;
                int blockIndex = FindIndexFromBlocks(hitObject);

                if (blockIndex >= 0)
                {
                    selectedBlock = blocks[blockIndex];
                    selectedBlock.SetChildGameObjectActive("cube", true);
                }
                else if (selectedBlock != null)
                {
                    selectedBlock.SetChildGameObjectActive("cube", false);
                    selectedBlock = null;
                }
            }
            else if (selectedBlock != null)
            {
                selectedBlock.SetChildGameObjectActive("cube", false);
                selectedBlock = null;
            }
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

    public int FindIndexFromBlocks(GameObject gameObject)
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i].GameObject == gameObject)
                return i;
        }

        return -1;
    }
}
