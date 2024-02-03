using App.Code.Model.Proto.Entities.Interfaces;
using UnityEngine;

namespace App.Code.Model.Proto.Data
{
    public struct Point : IPoint
    {
        public Vector2 Position { get; }

        public Point(Vector2 position)
        {
            Position = position;
        }
    }
}