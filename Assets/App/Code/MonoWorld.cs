using System.Linq;
using App.Code.Model.Proto;
using App.Code.Model.Proto.Field;
using App.Code.Tools;
using App.Code.View;
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
                switch (CheckState(_views[i], _space.Entities[i]))
                {
                    case ViewState.Add:
                        _views[i] = _source.Create<AsteroidView>(_space.Entities[i].Position);
                        break;
                    case ViewState.Remove:
                        Destroy(_views[i].gameObject);
                        break;
                    case ViewState.Update:
                        _views[i].Refresh(_space.Entities[i].Position);
                        break;
                }
            }
        }

        private static ViewState CheckState(MonoView view, Entity entity)
        {
            return view 
                ? entity == null 
                    ? ViewState.Remove 
                    : ViewState.Update
                : entity == null 
                    ? ViewState.Empty 
                    : ViewState.Add;
        }
    }
}