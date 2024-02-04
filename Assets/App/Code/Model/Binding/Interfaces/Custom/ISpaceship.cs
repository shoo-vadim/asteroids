using UnityEngine;

namespace App.Code.Model.Binding.Interfaces.Custom
{
    public interface ISpaceship : IElement
    {
        public Vector2 Direction { get; }
    }
}