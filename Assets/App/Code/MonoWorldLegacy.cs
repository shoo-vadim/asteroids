using System.Linq;
using App.Code.Model.Logical.Field;
using App.Code.Model.Proto;
using App.Code.Settings;
using App.Code.Tools;
using App.Code.View;
using App.Code.View.Pool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code
{
    [RequireComponent(typeof(ViewPool))]
    public class MonoWorldLegacy : MonoBehaviour
    {
        private SpaceModel _space;
        
        private EntitiesGenerator _generator;
        private ViewPool _pool;
        private MonoView[] _views;
        
        private Camera _camera;
        
        private void Awake()
        {
            var max = byte.MaxValue;
            var field = new GameField(30, 15);
            _camera = GetComponentInChildren<Camera>();
            _generator = new EntitiesGenerator(field, new Range<float>(3, 5));
            _space = new SpaceModel(field, _generator.CreateRandomEntities(10, max).ToArray());
            _views = new MonoView[max];
            _pool = GetComponent<ViewPool>();
        }

        private void Update()
        {
            _space.Update(Time.deltaTime);
            HandleInput();
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

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _space.DestroyIntersected(
                    _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
            }
            
            if (Keyboard.current.aKey.isPressed)
            {
                _space.Spaceship.ApplyRotation(-10 * Time.deltaTime);
            }

            if (Keyboard.current.dKey.isPressed)
            {
                _space.Spaceship.ApplyRotation(+10 * Time.deltaTime);
            }

            if (Keyboard.current.wKey.isPressed)
            {
                _space.Spaceship.ApplyThrust(-10);
            }
        }
    }
}