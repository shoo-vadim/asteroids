using System;
using System.Linq;
using UnityEngine;

namespace App.Code.View.Source
{
    public class ViewSource : MonoBehaviour
    {
        [SerializeField] private MonoView[] _views;

        private ViewRoot _root;

        private void Awake()
        {
            if (!(_root = GetComponentInChildren<ViewRoot>()))
            {
                throw new InvalidOperationException("Unable to find ViewRoot markup down in hierarchy");
            }
        }

        public TView Create<TView>(Vector2 position) where TView : MonoView, new()
        {
            var prefab = _views.Single(v => v.GetType() == typeof(TView));
            if (!prefab)
            {
                throw new ArgumentException($"Unable to find prefab of type {typeof(TView)}");
            }
            
            var view = Instantiate(prefab, _root.transform);
            view.Refresh(position);
            
            return view as TView;
        }
    }
}