using System;
using App.Code.Model;
using App.Code.Model.Custom;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces;
using App.Code.Model.Interfaces.Base;
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
            new ShipSettings(
                180,
                10,
                new BulletSettings(2, 0.5f, 16),
                new LaserSettings(5, 2f)),
            new AsteroidSettings(
                new Range<float>(1, 5), 
                new Range<float>(5, 10))
        );

        private ViewElements _view;
        
        private ISpaceship _spaceship;
        private ILaser _laser;
        private ISpace _space;
        private ISource<Asteroid> _asteroids;
        private ISource<Bullet> _bullets;

        private void Start()
        {
            if (!TryGetComponent<ViewPool>(out var pool))
            {
                throw new InvalidOperationException($"Unable to find {typeof(ViewPool).FullName} component!");
            }
            
            var field = new GameField(30, 15);
            
            _spaceship = new Spaceship(Vector2.zero, Vector2.up, Vector2.zero, 1f);
            _laser = new LaserModel(_settings.Spaceship.Laser);
            _asteroids = new AsteroidModel(field, _settings.Asteroid);
            _bullets = new BulletModel(field, _settings.Spaceship.Bullet, _asteroids as AsteroidModel);
            
            _space = new SpaceModel(field, 
                _settings, 
                _spaceship as Spaceship,
                _laser as LaserModel,
                _asteroids as AsteroidModel, 
                _bullets as BulletModel);
            
            _space.GameOver += OnGameOver;
            
            _view = new ViewElements(pool);
            
            _view.CreateSpaceship(_spaceship);
            
            _asteroids.Create += _view.CreateAsteroid;
            _asteroids.Remove += _view.RemoveAsteroid;

            _bullets.Create += _view.CreateBullet;
            _bullets.Remove += _view.RemoveBullet;

            _space.Build(10);
        }

        private void OnDestroy()
        {
            _view.RemoveSpaceship(_spaceship);
            
            _asteroids.Create -= _view.CreateAsteroid;
            _asteroids.Remove -= _view.RemoveAsteroid;
            
            _bullets.Create -= _view.CreateBullet;
            _bullets.Remove -= _view.RemoveBullet;
        }

        private void HandleInput()
        {
            if (Keyboard.current.dKey.isPressed)
            {
                _spaceship.ApplyRotation(+_settings.Spaceship.Rotation * Time.deltaTime); 
            }

            if (Keyboard.current.aKey.isPressed)
            {
                _spaceship.ApplyRotation(-_settings.Spaceship.Rotation * Time.deltaTime);
            }

            if (Keyboard.current.wKey.isPressed)
            {
                _spaceship.ApplyThrust(+_settings.Spaceship.Thrust * Time.deltaTime);
            }
            
            if (Keyboard.current.sKey.isPressed)
            {
                _spaceship.ApplyThrust(-_settings.Spaceship.Thrust * Time.deltaTime);
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

        private void OnGameOver()
        {
            Debug.Log("GameOver");
        }
    }
}