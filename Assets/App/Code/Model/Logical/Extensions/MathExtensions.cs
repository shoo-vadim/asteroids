using UnityEngine;

namespace App.Code.Model.Logical.Extensions
{
    public static class MathExtensions
    {


        public static Vector2 GetRotated(this Vector2 vector, float degrees)
        {
            return Quaternion.AngleAxis(degrees, Vector3.back) * vector;
        }
    }
}