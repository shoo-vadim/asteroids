using System.Linq;
using App.Code.Model.Proto;
using App.Code.Model.Proto.Field;
using App.Code.Tools;
using App.Code.View;
using App.Code.View.Custom;
using App.Code.View.Entities;
using App.Code.View.Source;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code
{
    [RequireComponent(typeof(ViewSource))]
    public class MonoWorld : MonoBehaviour
    {
        private SpaceModel _space;

        private EntitiesGenerator _generator;
        private ViewSource _source;
        private MonoView[] _views;
        
        private void Awake()
        {
            var max = byte.MaxValue;
            var field = new GameField(30, 15);
            _generator = new EntitiesGenerator(field, new Range<float>(3, 5));
            _space = new SpaceModel(field, _generator.CreateRandomEntities(10, max).ToArray());
            _views = new MonoView[max];
            _source = GetComponent<ViewSource>();
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

        private void DestroyView(ref MonoView view)
        {
            Destroy(view.gameObject);
            view = null;
        }

        private MonoView CreateViewFromEntity(Entity entity)
        {
            return entity.IsFragment
                ? _source.Create<FragmentView>(entity.Position)
                : _source.Create<AsteroidView>(entity.Position);
        }

        private bool IsAppropriateView(MonoView view, Entity entity)
        {
            return entity.IsFragment && view is FragmentView 
                   || !entity.IsFragment && view is AsteroidView; 
        }

        private void RefreshByIndex(int i)
        {
            ref var view = ref _views[i];
            ref var entity = ref _space.Entities[i];

            if (!view)
            {
                if (entity != null)
                {
                    view = CreateViewFromEntity(entity);
                }
            }
            else
            {
                if (entity != null)
                {
                    if (IsAppropriateView(view, entity))
                    {
                        view.Refresh(entity.Position);
                    }
                    else
                    {
                        DestroyView(ref view);
                        view = CreateViewFromEntity(entity);
                    }                    
                }
                else
                {
                    DestroyView(ref view);
                }
            }
        }
    }
}