using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Entities.Base;
using App.Code.Model.Logical.Extensions;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Spaceship : Body, IElementDirectionable
    {
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