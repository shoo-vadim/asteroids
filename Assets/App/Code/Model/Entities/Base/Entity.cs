using System;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Logical.Field;
using UnityEngine;

namespace App.Code.Model.Entities.Base
{
    public class Entity : IPositionable
    {
        public event Action<Vector2> UpdatePosition;
        
        public Vector2 Position { get; private set; }
        
        public Vector2 Movement { get; protected set; }

        protected Entity(Vector2 position, Vector2 movement)
        {
            Position = position;
            Movement = movement;
        }
        
        public void ApplyMovement(float deltaTime, GameField field)
        {
            Position += Movement * deltaTime;
            
            if (field.GetMirroredPosition(Position) is (true, var position))
            {
                Position = position;
            }
            
            UpdatePosition?.Invoke(Position);
        }
    }
}