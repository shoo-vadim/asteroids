using System;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Logical.Field;
using UnityEngine;

namespace App.Code.Model.Entities.Base
{
    public class Element : IElement
    {
        public event Action Update;
        
        public Vector2 Position { get; private set; }
        
        public Vector2 Movement { get; protected set; }

        protected Element(Vector2 position, Vector2 movement)
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
            
            TriggerUpdate();
        }

        protected void TriggerUpdate() => Update?.Invoke();
    }
}