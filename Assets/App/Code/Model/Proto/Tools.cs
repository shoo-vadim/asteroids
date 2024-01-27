using UnityEngine;

namespace App.Code.Model.Proto
{
    public static class Tools
    {
        public static Vector2 GetRandomDirection()
        {
            return Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector2.up;
        }
    }
}