using System;
using App.Code.Model.Entities.Base;
using App.Code.Model.Interfaces;
using App.Code.Model.Logical.Extensions;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Spaceship : Body, ISpaceship
    {
        public event Action<Vector2> DirectionChange;
        public event Action<Vector2> MovementChange;
        public Vector2 Direction { get; private set; }
        public Vector2 ShootingPoint => Position + Direction * 2;
        
        public Spaceship(Vector2 position, Vector2 direction, Vector2 movement, float radius) : 
            base(position, movement, radius)
        {
            Direction = direction;
        }

        public void ApplyRotation(float degrees)
        {
            Direction = Direction.GetRotated(degrees);
            DirectionChange?.Invoke(Direction);
        }

        public void ApplyThrust(float force)
        {
            Movement += Direction * force;
            MovementChange?.Invoke(Movement);
        }
    }
}