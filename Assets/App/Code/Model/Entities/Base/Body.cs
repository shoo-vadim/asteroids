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
    }
}