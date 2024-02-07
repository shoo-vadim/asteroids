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
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code
{
    [RequireComponent(typeof(ViewElements))]
    public class MonoWorld : MonoBehaviour
    {
        private readonly GameSettings _settings = new(
            new ShipSettings(
                180,
                10,
                new BulletSettings(2, 0.2f, 16),
                new LaserSettings(20, 2f)),
            new AsteroidSettings(
                new Range<float>(1, 5), 
                new Range<float>(5, 10))
        );

        private ViewElements _view;

        private GameField _field;
        private ISpaceship _spaceship;
        private ILaser _laser;
        private ISpace _space;
        private ISource<Asteroid> _asteroids;
        private ISource<Bullet> _bullets;

        private void Start()
        {
            if (!TryGetComponent(out _view))
            {
                throw new InvalidOperationException($"Unable to find {typeof(ViewElements).FullName} component!");
            }
            
            _field = new GameField(30, 15);
            
            _spaceship = new Spaceship(Vector2.zero, Vector2.up, Vector2.zero, 1f);
            _laser = new LaserModel(_settings.Spaceship.Laser);
            _asteroids = new AsteroidModel(_field, _settings.Asteroid);
            _bullets = new BulletModel(_field, _settings.Spaceship.Bullet, _asteroids as AsteroidModel);
            
            _space = new SpaceModel(
                _field, 
                _settings, 
                _spaceship as Spaceship,
                _laser as LaserModel,
                _asteroids as AsteroidModel, 
                _bullets as BulletModel);
            
            _space.GameOver += OnGameOver;

            BindView();

            _space.Build(10);
        }

        private void OnDestroy()
        {
            DropView();
        } 

        private void BindView()
        {
            _view.BindUI(_spaceship, _laser);
            _view.CreateSpaceship(_spaceship);
            
            _asteroids.Create += _view.CreateAsteroid;
            _asteroids.Remove += _view.RemoveAsteroid;

            _bullets.Create += _view.CreateBullet;
            _bullets.Remove += _view.RemoveBullet;
        }

        private void DropView()
        {
            _view.DropUI(_spaceship, _laser);
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
                    _view.CreateLaser(ray);
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