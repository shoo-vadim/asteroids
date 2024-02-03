using UnityEngine;

namespace App.Code.Model.Proto.Entities.Interfaces
{
    public interface ISpaceship
    {
        public Vector2 Direction { get; }

        public void ApplyRotation(float degrees);

        public void ApplyThrust(float force);
    }
}