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

        UpdateTextureCoordinates();
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

    private void UpdateTextureCoordinates()
    {
        
    }

    private Material LoadMaterial(string path)
    {
        return Resources.Load(path) as Material;
    }

    private Material GetMaterial(GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        return renderer.material;
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

        UpdateTextureCoordinates();
    }

    private void SetBookshelfMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_planks");
        SetMaterial(ChildGameObjects["bottom"], "block_planks");
        SetMaterial(ChildGameObjects["front"], "block_bookshelf");
        SetMaterial(ChildGameObjects["back"], "block_bookshelf");
        SetMaterial(ChildGameObjects["left"], "block_bookshelf");
        SetMaterial(ChildGameObjects["right"], "block_bookshelf");
    }

    private void SetBrickMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_brick");
        SetMaterial(ChildGameObjects["bottom"], "block_brick");
        SetMaterial(ChildGameObjects["front"], "block_brick");
        SetMaterial(ChildGameObjects["back"], "block_brick");
        SetMaterial(ChildGameObjects["left"], "block_brick");
        SetMaterial(ChildGameObjects["right"], "block_brick");
    }

    private void SetChestMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_chest_top");
        SetMaterial(ChildGameObjects["bottom"], "block_chest_top");
        SetMaterial(ChildGameObjects["front"], "block_chest");
        SetMaterial(ChildGameObjects["back"], "block_chest_side");
        SetMaterial(ChildGameObjects["left"], "block_chest_side");
        SetMaterial(ChildGameObjects["right"], "block_chest_side");
    }

    private void SetCraftingTableMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_crafting_table_top");
        SetMaterial(ChildGameObjects["bottom"], "block_planks");
        SetMaterial(ChildGameObjects["front"], "block_crafting_table_front");
        SetMaterial(ChildGameObjects["back"], "block_crafting_table_front");
        SetMaterial(ChildGameObjects["left"], "block_crafting_table_front");
        SetMaterial(ChildGameObjects["right"], "block_crafting_table_front");
    }

    private void SetDiamondMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_diamond");
        SetMaterial(ChildGameObjects["bottom"], "block_diamond");
        SetMaterial(ChildGameObjects["front"], "block_diamond");
        SetMaterial(ChildGameObjects["back"], "block_diamond");
        SetMaterial(ChildGameObjects["left"], "block_diamond");
        SetMaterial(ChildGameObjects["right"], "block_diamond");
    }

    private void SetDirtMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_dirt");
        SetMaterial(ChildGameObjects["bottom"], "block_dirt");
        SetMaterial(ChildGameObjects["front"], "block_dirt");
        SetMaterial(ChildGameObjects["back"], "block_dirt");
        SetMaterial(ChildGameObjects["left"], "block_dirt");
        SetMaterial(ChildGameObjects["right"], "block_dirt");
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
        SetMaterial(ChildGameObjects["top"], "block_hay_top");
        SetMaterial(ChildGameObjects["bottom"], "block_hay_top");
        SetMaterial(ChildGameObjects["front"], "block_hay_side");
        SetMaterial(ChildGameObjects["back"], "block_hay_side");
        SetMaterial(ChildGameObjects["left"], "block_hay_side");
        SetMaterial(ChildGameObjects["right"], "block_hay_side");
    }

    private void SetIceMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_ice");
        SetMaterial(ChildGameObjects["bottom"], "block_ice");
        SetMaterial(ChildGameObjects["front"], "block_ice");
        SetMaterial(ChildGameObjects["back"], "block_ice");
        SetMaterial(ChildGameObjects["left"], "block_ice");
        SetMaterial(ChildGameObjects["right"], "block_ice");
    }

    private void SetLogMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_log_top");
        SetMaterial(ChildGameObjects["bottom"], "block_log_top");
        SetMaterial(ChildGameObjects["front"], "block_log");
        SetMaterial(ChildGameObjects["back"], "block_log");
        SetMaterial(ChildGameObjects["left"], "block_log");
        SetMaterial(ChildGameObjects["right"], "block_log");
    }

    private void SetPlanksMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_planks");
        SetMaterial(ChildGameObjects["bottom"], "block_planks");
        SetMaterial(ChildGameObjects["front"], "block_planks");
        SetMaterial(ChildGameObjects["back"], "block_planks");
        SetMaterial(ChildGameObjects["left"], "block_planks");
        SetMaterial(ChildGameObjects["right"], "block_planks");
    }

    private void SetPumpkinMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_pumpkin_top");
        SetMaterial(ChildGameObjects["bottom"], "block_pumpkin_side");
        SetMaterial(ChildGameObjects["front"], "block_pumpkin_front");
        SetMaterial(ChildGameObjects["back"], "block_pumpkin_side");
        SetMaterial(ChildGameObjects["left"], "block_pumpkin_side");
        SetMaterial(ChildGameObjects["right"], "block_pumpkin_side");
    }

    private void SetStoneMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_stone");
        SetMaterial(ChildGameObjects["bottom"], "block_stone");
        SetMaterial(ChildGameObjects["front"], "block_stone");
        SetMaterial(ChildGameObjects["back"], "block_stone");
        SetMaterial(ChildGameObjects["left"], "block_stone");
        SetMaterial(ChildGameObjects["right"], "block_stone");
    }

    private void SetStoneBrickMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_stone_brick");
        SetMaterial(ChildGameObjects["bottom"], "block_stone_brick");
        SetMaterial(ChildGameObjects["front"], "block_stone_brick");
        SetMaterial(ChildGameObjects["back"], "block_stone_brick");
        SetMaterial(ChildGameObjects["left"], "block_stone_brick");
        SetMaterial(ChildGameObjects["right"], "block_stone_brick");
    }

    private void SetTNTMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_tnt_top");
        SetMaterial(ChildGameObjects["bottom"], "block_tnt_bottom");
        SetMaterial(ChildGameObjects["front"], "block_tnt_side");
        SetMaterial(ChildGameObjects["back"], "block_tnt_side");
        SetMaterial(ChildGameObjects["left"], "block_tnt_side");
        SetMaterial(ChildGameObjects["right"], "block_tnt_side");
    }

    private void SetWoolMaterials()
    {
        SetMaterial(ChildGameObjects["top"], "block_wool");
        SetMaterial(ChildGameObjects["bottom"], "block_wool");
        SetMaterial(ChildGameObjects["front"], "block_wool");
        SetMaterial(ChildGameObjects["back"], "block_wool");
        SetMaterial(ChildGameObjects["left"], "block_wool");
        SetMaterial(ChildGameObjects["right"], "block_wool");
    }
}
