using System;
using App.Code.Model;
using App.Code.Model.Interfaces;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
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
            new ShipSettings(
                Vector2.zero, 
                Vector2.up,
                180,
                10,
                new BulletSettings(2, 0.5f, 16),
                new LaserSettings(5, 2f)),
            new AsteroidsSettings(
                new Range<float>(1, 5), 
                new Range<float>(5, 10))
        );

        private ViewPool _pool;
        private ISpace _space;

        private void Start()
        {
            if (!(_pool = GetComponent<ViewPool>()))
            {
                throw new InvalidOperationException($"Unable to find {typeof(ViewPool).FullName} component!");
            }
            
            _space = new SpaceModel(new GameField(30, 15), _settings);
            _space.GameOver += OnGameOver;
            
            _space.Build(10);
        }

        private void HandleInput()
        {
            if (Keyboard.current.dKey.isPressed)
            {
                _space.Spaceship.ApplyRotation(+_settings.Spaceship.Rotation * Time.deltaTime); 
            }

            if (Keyboard.current.aKey.isPressed)
            {
                _space.Spaceship.ApplyRotation(-_settings.Spaceship.Rotation * Time.deltaTime);
            }

            if (Keyboard.current.wKey.isPressed)
            {
                _space.Spaceship.ApplyThrust(+_settings.Spaceship.Thrust * Time.deltaTime);
            }
            
            if (Keyboard.current.sKey.isPressed)
            {
                _space.Spaceship.ApplyThrust(-_settings.Spaceship.Thrust * Time.deltaTime);
            }

            if (Keyboard.current.shiftKey.wasPressedThisFrame)
            {
                if (_space.TryApplyLaserShot(out var ray))
                {
                    Debug.DrawRay(ray.origin, ray.direction);
                }
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (!_space.TryApplyBulletShot())
                {
                    Debug.Log("AUDIO: Weapon is not ready");
                }
            }
        }

        private void Update()
        {
            HandleInput();
            
            _space.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            // _space.ElementCreate -= OnElementCreate;
            // _space.ElementRemove -= OnElementRemove;
        }

        // private static ElementType GetElementType(IElement element)
        // {
        //     return element switch
        //     {
        //         IAsteroid { IsFragment: true } => 
        //             ElementType.Fragment, 
        //         IAsteroid { IsFragment: false } => 
        //             ElementType.Asteroid,
        //         IBullet => 
        //             ElementType.Bullet,
        //         ISpaceship => 
        //             ElementType.Spaceship,
        //         _ => throw new ArgumentOutOfRangeException(nameof(element), element, null)
        //     };
        // }

        // private void OnElementCreate(IElement element)
        // {
        //     var view = _pool.Obtain(GetElementType(element), element.Position);
        //     
        //     // TODO: Rewrite binding
        //     if (element is ISpaceship spaceship)
        //     {
        //         spaceship.Update += () => view.transform.rotation =
        //                 Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, spaceship.Direction));
        //     }
        //
        //     element.Update += () => view.transform.position = element.Position;
        //     
        //     _views.Add(element, view);
        // }
        //
        // private void OnElementRemove(IElement element)
        // {
        //     if (!_views.TryGetValue(element, out var view))
        //     {
        //         throw new ArgumentException("Unable to find view for element");
        //     }
        //
        //     _views.Remove(element);
        //     _pool.Release(view);
        // }

        private void OnGameOver()
        {
            Debug.Log("GameOver");
        }
    }
}