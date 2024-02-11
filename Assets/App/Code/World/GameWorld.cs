using System;
using System.Collections.Generic;
using System.Linq;
using App.Code.Model;
using App.Code.Model.Custom;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Custom.Bullets;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces;
using App.Code.Model.Interfaces.Base;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using App.Code.View.Elements;
using App.Code.World.Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code.World
{
    [RequireComponent(typeof(GameElements))]
    public class GameWorld : MonoWorld<GameElements>
    {
        public event Action GameOver;

        [SerializeField] private GameSettings _settings;

        private GameField _field;

        private ISpaceship _spaceship;
        private ILaser _laser;
        private ISpace _space;
        private ISource<Asteroid> _asteroids;
        private (ISource<Bullet> player, ISource<Bullet> enemy) _bullets;

        private void Start()
        {
            _field = CreateGameFieldFromCamera();
            
            var spaceship = new Spaceship(Vector2.zero, Vector2.up, Vector2.zero, 1f);
            var laser = new LaserModel(_settings.Spaceship.Laser);
            var builder = new AsteroidBuilder(_field.GetRandomPositionOnBorder, _settings.Asteroid.Speed);
            var asteroidCollection = builder.BuildCollection(10).ToArray(); 
            var asteroids = new AsteroidModel(_field, _settings.Asteroid, builder, asteroidCollection);
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

            BindView(asteroidCollection);
        }

        private void OnDestroy()
        {
            DropView();
        }

        private void BindView(IEnumerable<Asteroid> asteroidCollection)
        {
            foreach (var asteroid in asteroidCollection)
            {
                View.CreateAsteroid(asteroid);
            }
            
            View.BindUI(_spaceship, _laser);
            View.CreateSpaceship(_spaceship);
            
            _asteroids.Create += View.CreateAsteroid;
            _asteroids.Remove += View.RemoveAsteroid;

            _bullets.player.Create += View.CreateBullet;
            _bullets.player.Remove += View.RemoveBullet;
            _bullets.enemy.Create += View.CreateBullet;
            _bullets.enemy.Remove += View.RemoveBullet;
        }

        private void DropView()
        {
            View.DropUI(_spaceship, _laser);
            View.RemoveSpaceship(_spaceship);
            
            _asteroids.Create -= View.CreateAsteroid;
            _asteroids.Remove -= View.RemoveAsteroid;
            
            _bullets.player.Create -= View.CreateBullet;
            _bullets.player.Remove -= View.RemoveBullet;
            _bullets.enemy.Create -= View.CreateBullet;
            _bullets.enemy.Remove -= View.RemoveBullet;
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
                    View.CreateLaser(ray);
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

        private void OnGameOver() => GameOver?.Invoke();
    }
}