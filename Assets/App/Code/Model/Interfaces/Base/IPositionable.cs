using System;
using UnityEngine;

namespace App.Code.Model.Interfaces.Base
{
    public interface IPositionable
    {
        public event Action<Vector2> PositionChange;

        public Vector2 Position { get; }
    }
}