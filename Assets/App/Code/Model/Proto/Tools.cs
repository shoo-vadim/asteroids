using UnityEngine;

namespace App.Code.Model.Proto
{
    public static class Tools
    {
        public static Vector2 GetRotated(this Vector2 vector, int degrees) =>
            Quaternion.AngleAxis(degrees, Vector3.forward) * vector;
    }
}