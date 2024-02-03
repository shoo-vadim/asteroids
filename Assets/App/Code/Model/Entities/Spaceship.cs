using System;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Entities.Base;
using App.Code.Tools;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Spaceship : Body, IDirectionable
    {
        public event Action<Vector2> UpdateDirection;
        
        public Vector2 Direction { get; private set; }
        
        public Spaceship(Vector2 position, Vector2 movement, float radius) : 
            base(position, movement, radius)
        {
        }

        public void ApplyRotation(float degrees)
        {
            Direction = Direction.GetRotated(degrees);
            UpdateDirection?.Invoke(Direction);
        }
        
        public void ApplyThrust(float force)
        {
            Movement += Direction * force;
        }
    }
}