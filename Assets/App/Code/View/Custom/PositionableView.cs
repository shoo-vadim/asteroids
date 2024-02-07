using App.Code.Model.Interfaces.Base;
using UnityEngine;

namespace App.Code.View.Custom
{
    public class PositionableView : MonoBehaviour
    {
        public virtual void Bind(IPositionable positionable)
        {
            positionable.PositionChange += OnPositionChange;
        }
        
        public virtual void Drop(IPositionable positionable)
        {
            positionable.PositionChange -= OnPositionChange;
        }

        private void OnPositionChange(Vector2 position)
        {
            transform.position = position;
        }
    }
}