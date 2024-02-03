using System;
using UnityEngine;

namespace App.Code.Model.Binding.Interfaces
{
    public interface IElement
    {
        public event Action Update;
        
        public Vector2 Position { get; }
    }
}