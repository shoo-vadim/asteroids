using UnityEngine;

namespace App.Code.Model.Entities.Base
{
    public class Body : Entity
    {
        public float Radius { get; }
        
        protected Body(Vector2 position, Vector2 movement, float radius) : 
            base(position, movement)
        {
            Radius = radius;
        }
        public bool HasIntersectionWithRay(Ray2D ray)
        {
            var dot = Vector2.Dot(Position - ray.origin,ray.direction);
            var closest = ray.origin + ray.direction * dot;
            return dot > 0 && (closest - Position).sqrMagnitude < Mathf.Pow(Radius, 2);
        }

        public bool HasIntersectionWithPoint(Vector2 point)
        {
            return (Position - point).sqrMagnitude < Mathf.Pow(Radius, 2);
        }

        public bool HasIntersectionWithBody(Body body)
        {
            return (Position - body.Position).sqrMagnitude < Mathf.Pow(Radius + body.Radius, 2);
        }
    }
}