using System.Linq;
using App.Code.Model.Proto;
using App.Code.Model.Proto.Field;
using App.Code.Tools;
using App.Code.View;
using App.Code.View.Source;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code
{
    [RequireComponent(typeof(ViewPool))]
    public class MonoWorld : MonoBehaviour
    {
        private SpaceModel _space;

        private EntitiesGenerator _generator;
        private ViewPool _pool;
        private MonoView[] _views;
        
        private void Awake()
        {
            var max = byte.MaxValue;
            var field = new GameField(30, 15);
            _generator = new EntitiesGenerator(field, new Range<float>(3, 5));
            _space = new SpaceModel(field, _generator.CreateRandomEntities(10, max).ToArray());
            _views = new MonoView[max];
            _pool = GetComponent<ViewPool>();
        }

        private void Update()
        {
            _space.Update(Time.deltaTime);
            HandleInput();
            RefreshViews();
        }

        private void HandleInput()
        {
            if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
            {
                _space.AddEntity(_generator.CreateRandomSingleEntity());
            }
            
            if (Keyboard.current.numpadMinusKey.wasPressedThisFrame)
            {
                _space.DestroyRandomEntity();
            }
        }

        private void RefreshViews()
        {
            for (var i = 0; i < _space.Entities.Length; i++)
            {
                RefreshByIndex(i);
            }
        }

        private void RefreshByIndex(int i)
        {
            ref var view = ref _views[i];
            ref var entity = ref _space.Entities[i];

            if (!view)
            {
                if (entity != null)
                {
                    view = _pool.Obtain(entity.ElementType, entity.Position);
                }
            }
            else
            {
                if (entity != null)
                {
                    if (view.ElementType == entity.ElementType)
                    {
                        view.Refresh(entity.Position);
                    }
                    else
                    {
                        _pool.Release(view);
                        view = _pool.Obtain(entity.ElementType, entity.Position);
                    }                    
                }
                else
                {
                    _pool.Release(view);
                    view = null;
                }
            }
        }
    }
}