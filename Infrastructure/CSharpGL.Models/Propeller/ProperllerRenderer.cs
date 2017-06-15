﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CSharpGL.Models
{
    //
    //        2-------------------3
    //      / .                  /|
    //     /  .                 / |
    //    /   .                /  |
    //   /    .               /   |
    //  /     .              /    |
    // 6--------------------7     |
    // |      .             |     |
    // |      0 . . . . . . |. . .1
    // |     .              |    /
    // |    .               |   /
    // |   .                |  /
    // |  .                 | /
    // | .                  |/
    // 4 -------------------5
    //
    /// <summary>
    /// 
    /// </summary>
    public class ProperllerRenderer : RendererBase, IRenderable
    {
        private const float height = 0.2f;
        /// <summary>
        /// eight vertexes.
        /// </summary>
        private static readonly vec3[] positions = new vec3[]
        {
            new vec3(-1, -height, -1),// 0
            new vec3(-1, -height, +1),// 1
            new vec3(-1, +height, -1),// 2
            new vec3(-1, +height, +1),// 3
            new vec3(+1, -height, -1),// 4
            new vec3(+1, -height, +1),// 5
            new vec3(+1, +height, -1),// 6
            new vec3(+1, +height, +1),// 7
        };

        private static readonly vec3 red = new vec3(1, 0, 0);
        private static readonly vec3 green = new vec3(0, 1, 0);
        private static readonly vec3 blue = new vec3(0, 0, 1);

        private const float darkFactor = 3.0f;
        private static readonly vec3[] colors = new vec3[]
        {
            (red / darkFactor + green / darkFactor + blue / darkFactor),
            (red / darkFactor + green / darkFactor + blue),
            (red / darkFactor + green + blue / darkFactor),
            (red / darkFactor + green + blue),
            (red + green / darkFactor + blue / darkFactor),
            (red + green / darkFactor + blue),
            (red + green + blue / darkFactor),
            (red + green + blue),
        };

        /// <summary>
        /// render in GL_QUADS.
        /// </summary>
        private static readonly byte[] indexes = new byte[24]
        {
            1, 3, 7, 5, 0, 4, 6, 2,
            2, 6, 7, 3, 0, 1, 5, 4,
            4, 5, 7, 6, 0, 2, 3, 1,
        };

        #region IRenderable 成员

        public void Render(RenderEventArgs arg)
        {
            this.RotationAngle += 3f;

            this.LegacyMVP(arg);

            GL.Instance.Begin((uint)DrawMode.Quads);
            for (int i = 0; i < indexes.Length; i++)
            {
                vec3 color = colors[indexes[i]];
                GL.Instance.Color3f(color.x, color.y, color.z);
                vec3 position = positions[indexes[i]];
                GL.Instance.Vertex3f(position.x, position.y, position.z);
            }
            GL.Instance.End();
        }

        #endregion
    }
}