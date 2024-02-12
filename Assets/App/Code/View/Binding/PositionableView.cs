using App.Code.Model.Interfaces.Base;
using UnityEngine;

namespace App.Code.View.Binding
{
    public class PositionableView<TModel> : BindableView<TModel> where TModel : IPositionable
    {
        public override void Bind(TModel model)
        {
            model.PositionChange += OnPositionChange;
        }

        public override void Drop(TModel model)
        {
            model.PositionChange -= OnPositionChange;
        }
        
        private void OnPositionChange(Vector2 position)
        {
            transform.position = position;
        }
    }
}