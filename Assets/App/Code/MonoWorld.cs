using System;
using App.Code.Model;
using App.Code.Model.Custom;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Custom.Bullets;
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
        private (ISource<Bullet> player, ISource<Bullet> enemy) _bullets;

        private void Start()
        {
            if (!TryGetComponent(out _view))
            {
                throw new InvalidOperationException($"Unable to find {typeof(ViewElements).FullName} component!");
            }

            _field = CreateGameFieldFromCamera(GetComponentInChildren<Camera>());
            
            var spaceship = new Spaceship(Vector2.zero, Vector2.up, Vector2.zero, 1f);
            var laser = new LaserModel(_settings.Spaceship.Laser);
            var asteroids = new AsteroidModel(_field, _settings.Asteroid);
            var bullets = (
                player: new PlayerBulletModel(_field, _settings.Spaceship.Bullet, asteroids),
                enemy: new EnemyBulletModel(_field, spaceship));
            var enemy = new EnemyModel(bullets.enemy);
            
            var space = new SpaceModel(_field, _settings, spaceship, laser, asteroids, bullets.player, enemy);
            
            space.GameOver += OnGameOver;
            bullets.enemy.GameOver += OnGameOver;

            _spaceship = spaceship;
            _laser = laser;
            _space = space;
            _asteroids = asteroids;
            _bullets = (bullets.player, bullets.enemy);
            
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

            _bullets.player.Create += _view.CreateBullet;
            _bullets.player.Remove += _view.RemoveBullet;
            _bullets.enemy.Create += _view.CreateBullet;
            _bullets.enemy.Remove += _view.RemoveBullet;
        }

        private void DropView()
        {
            _view.DropUI(_spaceship, _laser);
            _view.RemoveSpaceship(_spaceship);
            
            _asteroids.Create -= _view.CreateAsteroid;
            _asteroids.Remove -= _view.RemoveAsteroid;
            
            _bullets.player.Create -= _view.CreateBullet;
            _bullets.player.Remove -= _view.RemoveBullet;
            _bullets.enemy.Create -= _view.CreateBullet;
            _bullets.enemy.Remove -= _view.RemoveBullet;
        }

        private static GameField CreateGameFieldFromCamera(Camera camera)
        {
            if (camera is not { orthographicSize: var size})
            {
                throw new InvalidOperationException("Camera is not found");
            }

            return new GameField(size * camera.aspect, size);
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

            if (Keyboard.current.ctrlKey.wasPressedThisFrame)
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