using UnityEngine;

namespace App.Code.Model.Proto.Entities.Interfaces
{
    public interface IMovable
    {
        public Vector2 Movement { get; }

        public void ApplyMovement(float deltaTime);
    }
}