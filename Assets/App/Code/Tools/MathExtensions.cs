using App.Code.Model.Entities.Base;
using UnityEngine;

namespace App.Code.Tools
{
    public static class MathExtensions
    {
        private static float Sqr(float f) => f * f;
        
        public static bool HasIntersectionWithRay(this Body target, Ray2D ray)
        {
            var dot = Vector2.Dot(target.Position - ray.origin,ray.direction);
            var closest = ray.origin + ray.direction * dot;
            return dot > 0 && (closest - target.Position).sqrMagnitude < Sqr(target.Radius);
        }

        public static bool HasIntersectionWithPoint(this Body target, Vector2 point)
        {
            return (target.Position - point).sqrMagnitude < Sqr(target.Radius);
        }

        public static bool HasIntersectionWithBody(this Body target, Body body)
        {
            return (target.Position - body.Position).sqrMagnitude < Sqr(target.Radius + body.Radius);
        }

        public static Vector2 GetRotated(this Vector2 vector, float degrees)
        {
            return Quaternion.AngleAxis(degrees, Vector3.back) * vector;
        }
    }
}