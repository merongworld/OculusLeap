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
    private BlockType type;
    private Vector3 position;
    private Vector3 scale;

    public GameObject GameObject { get; set; }
    public Dictionary<string, GameObject> ChildGameObjects { get; set; }

    public Block()
    {
        type = BlockType.Grass;
        GameObject = Instantiate(Resources.Load("Block")) as GameObject;
        ChildGameObjects = new Dictionary<string, GameObject>();
        AddChildGameObjects();
    }

    public Block(BlockType type)
    {
        this.type = type;

        GameObject = Instantiate(Resources.Load("Block")) as GameObject;
        ChildGameObjects = new Dictionary<string, GameObject>();
        AddChildGameObjects();
    }

    private void OnDestroy()
    {
        Destroy(GameObject);
    }

    private void AddChildGameObjects()
    {
        Transform transform = GameObject.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject childGameObject = transform.GetChild(i).gameObject;
            ChildGameObjects.Add(childGameObject.name.ToLower(), childGameObject);
        }
    }

    public void Move(Vector3 position)
    {
        this.position = position;

        Transform transform = GameObject.GetComponent<Transform>();
        transform.position = position;
    }

    // Scale vector components must have integer values
    public void Scale(Vector3 scale)
    {
        this.scale = scale;

        Transform transform = GameObject.GetComponent<Transform>();
        transform.localScale = scale * 0.1f;

        // Update texture coordinates
    }

    public BlockType Type
    {
        get { return type; }
        set
        {
            type = value;
            SetMaterials();
        }
    }

    private void SetMaterials()
    {
        switch (type)
        {
            case BlockType.Bookshelf: break;
            case BlockType.Brick: break;
            case BlockType.Chest: break;
            case BlockType.CraftingTable: break;
            case BlockType.Diamond: break;
            case BlockType.Dirt: break;
            case BlockType.Grass: break;
            case BlockType.Hay: break;
            case BlockType.Ice: break;
            case BlockType.Log: break;
            case BlockType.Planks: break;
            case BlockType.Pumpkin: break;
            case BlockType.Stone: break;
            case BlockType.StoneBrick: break;
            case BlockType.TNT: break;
            case BlockType.Wool: break;
            default: break;
        }

        // Update texture coordinates
    }
}
