using UnityEngine;

namespace App.Code.Settings
{
    public class ShipSettings
    {
        public Vector2 Position { get; }
        
        public Vector2 Direction { get; }

        public ShipSettings(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}