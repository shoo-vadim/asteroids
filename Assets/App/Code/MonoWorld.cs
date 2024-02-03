using System;
using System.Collections.Generic;
using App.Code.Model;
using App.Code.Model.Binding;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Logical.Field;
using App.Code.Tools;
using App.Code.View;
using App.Code.View.Pool;
using UnityEngine;

namespace App.Code
{
    [RequireComponent(typeof(ViewPool))]
    public class MonoWorld : MonoBehaviour
    {
        private readonly Dictionary<IElement, MonoView> _views = new();
        
        private OpenSpace _space;
        private ViewPool _pool;

        private void Start()
        {
            if (!(_pool = GetComponent<ViewPool>()))
            {
                throw new InvalidOperationException($"Unable to find {typeof(ViewPool).FullName} component!");
            }
            
            _space = new OpenSpace(
                new GameField(30, 15), 
                new GameSettings(
                    1f,
                    new Range<float>(1, 5)
                ));
            
            _space.ElementCreate += OnElementCreate;
            _space.ElementRemove += OnElementRemove;
            
            _space.BuildWorld();
        }

        private void Update()
        {
            _space.ApplyDelta(Time.deltaTime);
        }
        
        private void OnDestroy()
        {
            _space.ElementCreate -= OnElementCreate;
            _space.ElementRemove -= OnElementRemove;
        }
        
        private void OnElementCreate(ElementType elementType, IElement element)
        {
            var view = _pool.Obtain(elementType, element.Position);
            
            if (element is IElementDirectionable directionable)
            {
                directionable.Update += () => view.transform.rotation = Quaternion.LookRotation(directionable.Direction);
            }

            element.Update += () => view.transform.position = element.Position;
            
            _views.Add(element, view);
        }

        private void OnElementRemove(IElement element)
        {
            if (!_views.TryGetValue(element, out var view))
            {
                throw new ArgumentException("Unable to find view for element");
            }
            
            _pool.Release(view);
        }
    }
}