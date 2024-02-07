using System;
using UnityEngine;

namespace App.Code.Model.Interfaces.Base
{
    public interface IDirectionable
    {
        public event Action<Vector2> DirectionChange;

        public Vector2 Direction { get; }
    }
}