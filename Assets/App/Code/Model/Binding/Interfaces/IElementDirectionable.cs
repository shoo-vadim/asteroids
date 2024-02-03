using UnityEngine;

namespace App.Code.Model.Binding.Interfaces
{
    public interface IElementDirectionable : IElement
    {
        public Vector2 Direction { get; }
    }
}