using UnityEngine;

namespace App.Code.Model
{
    public struct Laser
    {
        public Vector2 Source;
        public Vector2 Destination;

        public Laser(Vector2 source, Vector2 destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}