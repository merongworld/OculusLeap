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
        GameObject = Instantiate(Resources.Load("Block")) as GameObject;
        ChildGameObjects = new Dictionary<string, GameObject>();
        AddChildGameObjects();
        Type = BlockType.Grass;
    }

    public Block(BlockType type)
    {
        GameObject = Instantiate(Resources.Load("Block")) as GameObject;
        ChildGameObjects = new Dictionary<string, GameObject>();
        AddChildGameObjects();
        Type = type;
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

    // Scale vector components must have positive integer values
    public void Scale(Vector3 scale)
    {
        Vector3 truncatedScale = MathUtils.truncate(scale);
        if (MathUtils.hasZero(truncatedScale)) return;

        Transform transform = GameObject.GetComponent<Transform>();
        transform.localScale = truncatedScale * 0.1f;

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

    private Material LoadMaterial(string path)
    {
        return Resources.Load(path) as Material;
    }

    private void SetMaterial(GameObject gameObject, string path)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material = LoadMaterial(path);
    }

    private void SetMaterials()
    {
        switch (type)
        {
            case BlockType.Bookshelf: SetBookshelfMaterials(); break;
            case BlockType.Brick: SetBrickMaterials(); break;
            case BlockType.Chest: SetChestMaterials(); break;
            case BlockType.CraftingTable: SetCraftingTableMaterials(); break;
            case BlockType.Diamond: SetDiamondMaterials(); break;
            case BlockType.Dirt: SetDirtMaterials(); break;
            case BlockType.Grass: SetGrassMaterials(); break;
            case BlockType.Hay: SetHayMaterials(); break;
            case BlockType.Ice: SetIceMaterials(); break;
            case BlockType.Log: SetLogMaterials(); break;
            case BlockType.Planks: SetPlanksMaterials(); break;
            case BlockType.Pumpkin: SetPumpkinMaterials(); break;
            case BlockType.Stone: SetStoneMaterials(); break;
            case BlockType.StoneBrick: SetStoneBrickMaterials(); break;
            case BlockType.TNT: SetTNTMaterials(); break;
            case BlockType.Wool: SetWoolMaterials(); break;
            default: break;
        }

        // Update texture coordinates
    }

    private void SetBookshelfMaterials()
    {

    }

    private void SetBrickMaterials()
    {

    }

    private void SetChestMaterials()
    {

    }

    private void SetCraftingTableMaterials()
    {

    }

    private void SetDiamondMaterials()
    {

    }

    private void SetDirtMaterials()
    {

    }

    private void SetGrassMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_grass_top");
        SetMaterial(ChildGameObjects["bottom"], "block_dirt");
        SetMaterial(ChildGameObjects["front"], "block_grass_side");
        SetMaterial(ChildGameObjects["back"], "block_grass_side");
        SetMaterial(ChildGameObjects["left"], "block_grass_side");
        SetMaterial(ChildGameObjects["right"], "block_grass_side");
    }

    private void SetHayMaterials()
    {

    }

    private void SetIceMaterials()
    {

    }

    private void SetLogMaterials()
    {

    }

    private void SetPlanksMaterials()
    {

    }

    private void SetPumpkinMaterials()
    {

    }

    private void SetStoneMaterials()
    {

    }

    private void SetStoneBrickMaterials()
    {

    }

    private void SetTNTMaterials()
    {

    }

    private void SetWoolMaterials()
    {

    }
}
