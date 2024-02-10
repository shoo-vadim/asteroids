using App.Code.Model.Interfaces;
using App.Code.View.Binding.Interfaces;
using UnityEngine;

namespace App.Code.View.UI.Dashboard
{
    public class SpaceshipUI : MonoBehaviour, IBindable<ISpaceship>
    {
        [SerializeField] private IndicatorView _indicatorCoordinates;
        [SerializeField] private IndicatorView _indicatorDirection;
        [SerializeField] private IndicatorView _indicatorSpeed;
        
        public void Bind(ISpaceship bind)
        {
            bind.PositionChange += OnPositionChange;
            bind.DirectionChange += OnDirectionChange;
            bind.MovementChange += OnMovementChange;
        }

        public void Drop(ISpaceship bind)
        {
            bind.PositionChange -= OnPositionChange;
            bind.DirectionChange -= OnDirectionChange;
            bind.MovementChange -= OnMovementChange;
        }
        
        private void OnPositionChange(Vector2 position) => 
            _indicatorCoordinates.Refresh($"{position.x:+00;-00} {position.y:+00;-00}");

        private void OnDirectionChange(Vector2 direction) =>
            _indicatorDirection.Refresh(Vector2.SignedAngle(Vector2.up, direction).ToString("+00;-00"));

        private void OnMovementChange(Vector2 movement) => 
            _indicatorSpeed.Refresh(movement.magnitude.ToString("00"));
    }
}