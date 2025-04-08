using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class GraphicsAssistant
    {

        /// <summary>
        /// Converts a 3D vector to a 2D vector by dividing by the z-coordinate.
        /// </summary>
        /// <param name="vector3">The 3d vector array.</param>
        /// <returns>a 2d vector array representing the 3d vector array as projected on the 2d plane.</returns>
        public Vector2[] ConvertVector3Array(Vector3[] vector3)
        {
            Vector2[] result = new Vector2[vector3.Length];
            for (int i = 0; i < vector3.Length; i++)
            {
                result[i] = ConvertVector3(vector3[i]);
            }
            return result;
        }


        /// <summary>
        /// Converts a 3D vector to a 2D vector by dividing by the z-coordinate.
        /// Represents the 3d vector as projected on the 2d plane.
        /// </summary>
        /// <param name="vector3">The 3d vector.</param>
        /// <returns>a 2d vector representing the 3d vector as projected on the 2d plane.</returns>
        public Vector2 ConvertVector3(Vector3 vector3)
        {
            // A common approach is to divide the x and y coordinates by the z-coordinate:
            //2D x = 3D x / 3D z
            //2D y = 3D y / 3D z

            float x = vector3.x / vector3.z;
            float y = vector3.y / vector3.z;

            return new Vector2(x, y);
        }


        public bool ComparePath3D(Vector3 a, Vector3 b, float tolerance)
        {
            bool result = false; 

            float deltaX = 0.0f;
            float deltaY = 0.0f;
            float deltaZ = 0.0f;
            // TODO: Compare the x and y coordinates of the two vectors

            deltaX = b.x - a.x;
            deltaY = b.y - a.y;
            deltaZ = b.z - a.z;

            return result;
        }


    }
}
