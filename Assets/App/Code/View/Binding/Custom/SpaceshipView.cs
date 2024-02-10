using App.Code.Model.Interfaces;
using UnityEngine;

namespace App.Code.View.Binding.Custom
{
    public class SpaceshipView : PositionableView<ISpaceship>
    {
        public override void Bind(ISpaceship model)
        {
            base.Bind(model);
            model.DirectionChange += OnDirectionChange;
        }

        public override void Drop(ISpaceship model)
        {
            base.Drop(model);
            model.DirectionChange -= OnDirectionChange;
        }

        private void OnDirectionChange(Vector2 direction)
        {
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, direction));
        }
    }
}