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
    private Vector3 position;
    private Vector3 scale;

    public BlockType Type { get; private set; }
    public GameObject GameObject { get; set; }
    public Dictionary<string, GameObject> ChildGameObjects { get; set; }

    public Block()
    {
        Type = BlockType.Grass;
        position = new Vector3();
        scale = new Vector3(1, 1, 1);

        GameObject = Instantiate(Resources.Load("Block")) as GameObject;
        ChildGameObjects = new Dictionary<string, GameObject>();
        AddChildGameObjects();
    }

    public Block(BlockType type)
    {
        Type = type;
        position = new Vector3();
        scale = new Vector3(1, 1, 1);

        GameObject = Instantiate(Resources.Load("Block")) as GameObject;
        ChildGameObjects = new Dictionary<string, GameObject>();
        AddChildGameObjects();
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

    public Vector3 Position
    {
        get { return position; }
        set
        {
            position = value;

            Transform transform = GameObject.GetComponent<Transform>();
            transform.position = position;
        }
    }

    public Vector3 Scale
    {
        get { return scale; }
        set
        {
            scale = value;

            Transform transform = GameObject.GetComponent<Transform>();
            transform.localScale = scale * 0.1f;
        }
    }

    public void SetType(BlockType type)
    {
        Type = type;
    }
}
