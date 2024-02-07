using App.Code.Model.Interfaces;
using UnityEngine;

namespace App.Code.View.Custom
{
    public class SpaceshipView : PositionableView
    {
        public void Bind(ISpaceship spaceship)
        {
            base.Bind(spaceship);
            spaceship.DirectionChange += OnDirectionChange;
        }

        public void Drop(ISpaceship spaceship)
        {
            base.Drop(spaceship);
            spaceship.DirectionChange += OnDirectionChange;
        }

        private void OnDirectionChange(Vector2 direction)
        {
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, direction));
        }
    }
}