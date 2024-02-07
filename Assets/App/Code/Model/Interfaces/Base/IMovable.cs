using System;
using UnityEngine;

namespace App.Code.Model.Interfaces.Base
{
    public interface IMovable
    {
        public event Action<Vector2> MovementChange;

        public Vector2 Movement { get; }
    }
}