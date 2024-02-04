using System;
using System.Collections.Generic;
using App.Code.Model;
using App.Code.Model.Binding;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using App.Code.View;
using App.Code.View.Pool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code
{
    [RequireComponent(typeof(ViewPool))]
    public class MonoWorld : MonoBehaviour
    {
        private readonly GameSettings _settings = new(
            1f,
            new Range<float>(1, 5),
            new ShipSettings(
                Vector2.zero, 
                Vector2.up,
                180,
                10)
        );
        
        private readonly Dictionary<IElement, MonoView> _views = new();
        
        private OpenSpace _space;
        private ViewPool _pool;

        private void Start()
        {
            if (!(_pool = GetComponent<ViewPool>()))
            {
                throw new InvalidOperationException($"Unable to find {typeof(ViewPool).FullName} component!");
            }
            
            _space = new OpenSpace(new GameField(30, 15), _settings);
            
            _space.ElementCreate += OnElementCreate;
            _space.ElementRemove += OnElementRemove;
            
            _space.BuildWorld();
        }

        private void Update()
        {
            if (Keyboard.current.dKey.isPressed)
            {
                _space.Spaceship.ApplyRotation(+_settings.Ship.Rotation * Time.deltaTime);
            }

            if (Keyboard.current.aKey.isPressed)
            {
                _space.Spaceship.ApplyRotation(-_settings.Ship.Rotation * Time.deltaTime);
            }

            if (Keyboard.current.wKey.isPressed)
            {
                _space.Spaceship.ApplyThrust(+_settings.Ship.Thrust * Time.deltaTime);
            }
            
            if (Keyboard.current.sKey.isPressed)
            {
                _space.Spaceship.ApplyThrust(-_settings.Ship.Thrust * Time.deltaTime);
            }
            
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
                directionable.Update += () => view.transform.rotation =
                        Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, directionable.Direction));
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

            _views.Remove(element);
            _pool.Release(view);
        }
    }
}