using App.Code.Model.Interfaces.Base;
using UnityEngine;

namespace App.Code.View.Binding
{
    public abstract class BindableView<TModel> : MonoBehaviour where TModel : IPositionable
    {
        public abstract void Bind(TModel model);
        public abstract void Drop(TModel model);
    }
}