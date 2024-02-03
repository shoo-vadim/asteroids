using System;
using App.Code.Model;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Logical.Field;
using App.Code.Tools;
using App.Code.View.Pool;
using UnityEngine;

namespace App.Code
{
    [RequireComponent(typeof(ViewPool))]
    public class MonoWorld : MonoBehaviour
    {
        private OpenSpace _space;
        private ViewPool _pool;

        private void Awake()
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
            _space.ElementCreate -= OnElementRemove;
        }
        
        private void OnElementCreate(IPositionable positionable)
        {
            // _pool.Obtain()
                
            if (positionable is IDirectionable directionable)
            {
                directionable.UpdateDirection += v => { };
            }

            positionable.UpdatePosition += v => { };
        }

        private void OnElementRemove(IPositionable positionable)
        {
            
        }
    }
}