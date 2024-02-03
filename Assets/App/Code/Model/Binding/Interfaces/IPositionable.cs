using System;
using UnityEngine;

namespace App.Code.Model.Binding.Interfaces
{
    public interface IPositionable
    {
        public event Action<Vector2> UpdatePosition;
    }
}