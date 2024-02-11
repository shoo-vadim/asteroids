using System;
using System.Collections.Generic;
using System.Linq;
using App.Code.Model;
using App.Code.Model.Custom;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Custom.Bullets;
using App.Code.Model.Custom.Enemies;
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
        public event Action Restart;

        [SerializeField] private GameSettings _settings;

        private GameField _field;

        private SpaceshipModel _spaceship;
        private ILaser _laser;
        private ISpace _space;
        private ISource<Asteroid> _asteroids;
        private ISource<Enemy> _enemies;
        private (ISource<Bullet> player, ISource<Bullet> enemy) _bullets;
        private IPointable[] _pointables;

        private int _points;

        private void Start()
        {
            _field = CreateGameFieldFromCamera();

            var spaceship = new Spaceship(Vector2.zero, Vector2.up, Vector2.zero, 1f);
            var spaceshipModel = new SpaceshipModel(_field, spaceship);
            var laser = new LaserModel(_settings.Spaceship.Laser);
            var builder = new AsteroidBuilder(_field.GetRandomPositionOnBorder, _settings.Asteroid.Speed);
            var asteroidCollection = builder.BuildCollection(10).ToArray(); 
            var asteroids = new AsteroidModel(_field, _settings.Asteroid, builder, asteroidCollection, spaceshipModel);
            var bulletsEnemy = new EnemyBulletModel(_field, spaceshipModel);
            var enemies = new EnemyModel(_field, bulletsEnemy, spaceshipModel, _settings.Enemy);
            var bulletsPlayer = new PlayerBulletModel(_field, _settings.Spaceship.Bullet, asteroids, enemies);

            var space = new SpaceModel(_settings, spaceshipModel, laser, asteroids, bulletsPlayer, enemies);

            _spaceship = spaceshipModel;
            _enemies = enemies;
            _laser = laser;
            _space = space;
            _asteroids = asteroids;
            _bullets = (bulletsPlayer, bulletsEnemy);
            _pointables = new IPointable[] { asteroids, enemies };

            BindLogic();
            BuildView(asteroidCollection, spaceship);
        }

        private void OnDestroy()
        {
            DropLogic();
            DropView();
        }

        private void BuildView(IEnumerable<Asteroid> asteroidCollection, ISpaceship spaceship)
        {
            View.CreateSpaceship(spaceship);
            
            foreach (var asteroid in asteroidCollection)
            {
                View.CreateAsteroid(asteroid);
            }

            BindView();
        }

        private void BindLogic()
        {
            _spaceship.Dead += OnGameOver;
            foreach (var p in _pointables) p.Point += OnPoint;
        }

        private void DropLogic()
        {
            _spaceship.Dead -= OnGameOver;
            foreach (var p in _pointables) p.Point -= OnPoint;
        }

        private void BindView()
        {
            View.UI.Restart += OnRestart;
            _spaceship.Create += CreateSpaceship;
            _spaceship.Remove += RemoveSpaceship;
            _enemies.Create += View.CreateEnemy;
            _enemies.Remove += View.RemoveEnemy;
            _asteroids.Create += View.CreateAsteroid;
            _asteroids.Remove += View.RemoveAsteroid;
            _bullets.player.Create += View.CreateBullet;
            _bullets.player.Remove += View.RemoveBullet;
            _bullets.enemy.Create += View.CreateBullet;
            _bullets.enemy.Remove += View.RemoveBullet;
        }

        private void DropView()
        {
            View.UI.Restart -= OnRestart;
            _spaceship.Create -= CreateSpaceship;
            _spaceship.Remove -= RemoveSpaceship;
            _enemies.Create -= View.CreateEnemy;
            _enemies.Remove -= View.RemoveEnemy;
            _asteroids.Create -= View.CreateAsteroid;
            _asteroids.Remove -= View.RemoveAsteroid;
            _bullets.player.Create -= View.CreateBullet;
            _bullets.player.Remove -= View.RemoveBullet;
            _bullets.enemy.Create -= View.CreateBullet;
            _bullets.enemy.Remove -= View.RemoveBullet;
        }

        private void CreateSpaceship(ISpaceship spaceship)
        {
            View.CreateSpaceship(spaceship);
            View.BindUI(_laser);
        }
        
        private void RemoveSpaceship(ISpaceship spaceship)
        {
            View.RemoveSpaceship(spaceship);
            View.DropUI(_laser);
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

        private void OnGameOver() => View.UI.ShowPoints(_points);

        private void OnRestart() => Restart?.Invoke(); 

        private void OnPoint() => _points++;
    }
}