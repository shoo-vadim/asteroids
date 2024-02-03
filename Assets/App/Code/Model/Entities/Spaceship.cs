using App.Code.Model.Proto.Entities.Base;
using App.Code.Tools;
using UnityEngine;

namespace App.Code.Model.Proto.Entities
{
    public class Spaceship : Body
    {
        public Vector2 Direction { get; private set; }
        
        protected Spaceship(Vector2 position, Vector2 movement, float radius) : 
            base(position, movement, radius)
        {
        }

        public void ApplyRotation(float degrees)
        {
            Direction = Direction.GetRotated(degrees);
        }
        
        public void ApplyThrust(float force)
        {
            Movement += Direction * force;
        }
    }
}