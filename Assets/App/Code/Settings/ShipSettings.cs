using UnityEngine;

namespace App.Code.Settings
{
    public class ShipSettings
    {
        public Vector2 Position { get; }
        
        public Vector2 Direction { get; }
        
        public float Rotation { get; }
        
        public float Thrust { get; }
        

        public ShipSettings(Vector2 position, Vector2 direction, float rotation, float thrust)
        {
            Position = position;
            Direction = direction;
            Rotation = rotation;
            Thrust = thrust;
        }
    }
}