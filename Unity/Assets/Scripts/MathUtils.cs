//
//  MathUtils.cs
//  OculusLeap
//
//  Created by merongworld on 10/02/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MathUtils
{
    public static Vector3 truncate(Vector3 vector)
    {
        int x = (int)vector.x;
        int y = (int)vector.y;
        int z = (int)vector.z;

        return new Vector3(x, y, z);
    }

    public static bool hasZero(Vector3 vector)
    {
        if (vector.x == 0) return true;
        else if (vector.y == 0) return true;
        else if (vector.z == 0) return true;

        return false;
    }
}
