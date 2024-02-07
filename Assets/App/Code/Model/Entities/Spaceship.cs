using System;
using App.Code.Model.Entities.Base;
using App.Code.Model.Logical.Extensions;
using App.Code.Settings;
using UnityEngine;
using ISpaceship = App.Code.Model.Interfaces.ISpaceship;

namespace App.Code.Model.Entities
{
    public class Spaceship : Body, ISpaceship
    {
        public event Action<Vector2> DirectionChange;
        public Vector2 Direction { get; private set; }
        
        public Spaceship(ShipSettings shipSettings, Vector2 movement, float radius) : 
            base(shipSettings.Position, movement, radius)
        {
            Direction = shipSettings.Direction;
        }

        public void ApplyRotation(float degrees)
        {
            Direction = Direction.GetRotated(degrees);
            TriggerUpdate();
        }

        public void ApplyThrust(float force)
        {
            Movement += Direction * force;
        }
    }
}