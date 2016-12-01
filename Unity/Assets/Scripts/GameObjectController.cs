//
//  GameObjectController.cs
//  OculusLeap
//
//  Created by merongworld on 11/24/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetParent(GameObject gameObject)
    {
        if (gameObject == null) { return null; }

        return gameObject.transform.parent.gameObject;
    }

    public List<GameObject> GetChildrenList(GameObject gameObject)
    {
        Transform transform = gameObject.transform;
        int childCount = transform.childCount;

        if (childCount == 0 || gameObject == null) { return null; }

        List<GameObject> childrenList = new List<GameObject>();
        for (int i = 0; i < childCount; i++)
        {
            GameObject childGameObject = transform.GetChild(i).gameObject;
            childrenList.Add(childGameObject);
        }

        return childrenList;
    }

    public Dictionary<string, GameObject> GetChildren(GameObject gameObject)
    {
        Transform transform = gameObject.transform;
        int childCount = transform.childCount;

        if (childCount == 0 || gameObject == null) { return null; }

        Dictionary<string, GameObject> children = new Dictionary<string, GameObject>();
        for (int i = 0; i < childCount; i++)
        {
            GameObject childGameObject = transform.GetChild(i).gameObject;
            children.Add(childGameObject.name, childGameObject);
        }

        return children;
    }
}
