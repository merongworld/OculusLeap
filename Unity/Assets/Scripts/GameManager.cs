//
//  GameManager.cs
//  OculusLeap
//
//  Created by merongworld on 10/28/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager Instance
    {
        get { return instance; }
    }
}
