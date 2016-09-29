//
//  GridRenderer.cs
//  OculusLeap
//
//  Created by merongworld on 09/16/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class GridRenderer : MonoBehaviour
{
    public int gridSize;

    private Material darkGray;
    private Material lightGray;

    private Material red;
    private Material green;
    private Material blue;

    void Start()
    {
        darkGray = new Material(Resources.Load("color_darkgray") as Material);
        lightGray = new Material(Resources.Load("color_lightgray") as Material);

        red = new Material(Resources.Load("color_red") as Material);
        green = new Material(Resources.Load("color_green") as Material);
        blue = new Material(Resources.Load("color_blue") as Material);
    }

    void OnPostRender()
    {
        DrawGrid(gridSize);
    }

    void DrawGrid(int size)
    {
        for (var i = -size; i <= size; i++)
        {
            for (var step = 0; step <= 10; step++)
            {
                if (i == 0 && step == 0)
                {
                    red.SetPass(0);
                    DrawLine(new Vector3(0, 0, 0), new Vector3(1, 0, 0));

                    green.SetPass(0);
                    DrawLine(new Vector3(0, 0, 0), new Vector3(0, 1, 0));

                    blue.SetPass(0);
                    DrawLine(new Vector3(0, 0, 0), new Vector3(0, 0, 1));

                    lightGray.SetPass(0);
                    DrawLine(new Vector3(-size, 0, 0), new Vector3(0, 0, 0));
                    DrawLine(new Vector3(1, 0, 0), new Vector3(size, 0, 0));

                    DrawLine(new Vector3(0, 0, -size), new Vector3(0, 0, 0));
                    DrawLine(new Vector3(0, 0, 1), new Vector3(0, 0, size));

                    continue;
                }
                else if (i + 0.1 * step == 0) continue; // To avoid floating-point error
                else if (step == 0) lightGray.SetPass(0);
                else darkGray.SetPass(0);

                DrawLine(new Vector3(-size, 0.0f, (float)(i + 0.1 * step)),
                    new Vector3(size, 0.0f, (float)(i + 0.1 * step)));
                DrawLine(new Vector3((float)(i + 0.1 * step), 0.0f, -size),
                    new Vector3((float)(i + 0.1 * step), 0.0f, size));

                if (i == size && step == 0) break;
            }
        }
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        GL.PushMatrix();
        {
            GL.Begin(GL.LINES);
            {
                GL.Vertex(start);
                GL.Vertex(end);
            } GL.End();
        } GL.PopMatrix();
    }
}
