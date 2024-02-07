using App.Code.Model.Entities.Base;
using App.Code.Model.Logical.Extensions;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Asteroid : Body
    {
        public bool IsFragment { get; }
        
        public Asteroid(Vector2 position, Vector2 movement, float radius, bool isFragment) : 
            base(position, movement, radius)
        {
            IsFragment = isFragment;
        }
        
        public Asteroid CreateFragment(float movementModifier)
        {
            return new Asteroid(
                Position,
                Movement.GetRotated(Random.Range(0, 360) * movementModifier),
                Radius,
                true);
        }
    }
}