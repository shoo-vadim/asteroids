using System;
using UnityEngine;

namespace App.Code.Model.Binding.Interfaces
{
    public interface IDirectionable : IPositionable
    {
        public event Action<Vector2> UpdateDirection;
    }
}