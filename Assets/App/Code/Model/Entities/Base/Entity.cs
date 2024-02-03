using App.Code.Model.Proto.Entities.Interfaces;
using UnityEngine;

namespace App.Code.Model.Proto.Entities.Base
{
    public class Entity : IPoint, IMovable
    {
        public Vector2 Position { get; private set; }
        
        public Vector2 Movement { get; protected set; }

        protected Entity(Vector2 position, Vector2 movement)
        {
            Position = position;
            Movement = movement;
        }

        public void ApplyMovement(float deltaTime)
        {
            Position += Movement * deltaTime;
        }
    }
}