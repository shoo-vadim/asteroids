using UnityEngine;

namespace App.Code.Model
{
    public struct Laser
    {
        public Vector2 Origin;
        public Vector2 Destination;

        public Laser(Vector2 origin, Vector2 destination)
        {
            Origin = origin;
            Destination = destination;
        }

        public bool HasIntersection(Circle circle)
        {
            var direction = (Destination - Origin).normalized;
            var dot = Vector2.Dot(circle.Position - Origin, direction);
            var closest = Origin + direction * dot;
            return dot > 0 
                   && (closest - circle.Position).sqrMagnitude < Mathf.Pow(circle.Radius, 2);
        }
    }
}