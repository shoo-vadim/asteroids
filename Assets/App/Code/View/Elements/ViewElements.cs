using System;
using App.Code.Model.Interfaces.Base;
using App.Code.View.Custom;
using App.Code.View.Pool;
using UnityEngine;

namespace App.Code.View.Elements
{
    public abstract class ViewElements : MonoBehaviour
    {
        private ViewPool _pool;

        protected virtual void Awake()
        {
            if (!TryGetComponent(out _pool))
            {
                throw new InvalidOperationException($"Unable to find {typeof(ViewPool).FullName} component!");
            }
        }

        protected TBind ObtainElement<TBind, TModel>(ElementType elementType, TModel model) 
            where TBind : BindableView<TModel>
            where TModel : IPositionable
        {
            var view = _pool.Obtain(elementType, model.Position);
            var bind = view.gameObject.AddComponent<TBind>();
            bind.Bind(model);
            return bind;
        }

        protected void ReleaseElement<TBind, TModel>(TBind bind, TModel model)
            where TBind : BindableView<TModel>
            where TModel : IPositionable
        {
            bind.Drop(model);
            Destroy(bind);
            _pool.Release(bind.GetComponent<MonoView>());
        }
    }
}