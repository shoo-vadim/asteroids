using System;
using System.Collections.Generic;
using System.Linq;
using App.Code.Model;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces;
using App.Code.Model.Logic;
using App.Code.Model.Logic.Asteroids;
using App.Code.Model.Logic.Bullets;
using App.Code.Model.Logic.Enemies;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using App.Code.Settings.Scriptables;
using App.Code.View.Elements;
using App.Code.World.Tools;
using UnityEngine;

namespace App.Code.World
{
    [RequireComponent(typeof(GameElements))]
    public class GameWorld : MonoWorld<GameElements>
    {
        public event Action Restart;
        
        [SerializeField] private GameSettings _settings;
        [SerializeField] private GameInputPreset _input;

        private GameField _field;

        private SpaceModel _space;
        private SpaceshipModel _spaceship;
        private ViewSources _sources;
        
        private ILaser _laser;

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
            _laser = laser;
            _space = space;

            _sources = new ViewSources(asteroids, enemies, bulletsPlayer, bulletsEnemy);

            BindLogic();
            BuildView(asteroidCollection, spaceship);
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void OnDestroy()
        {
            DropLogic();
            DropView();
        }

        private void BuildView(IEnumerable<Asteroid> asteroidCollection, ISpaceship spaceship)
        {
            View.CreateSpaceship(spaceship, _laser);
            foreach (var asteroid in asteroidCollection) View.CreateAsteroid(asteroid);
            
            BindView();
        }

        private void BindLogic()
        {
            _spaceship.Dead += OnGameOver;
            _space.Bind();
        }

        private void DropLogic()
        {
            _spaceship.Dead -= OnGameOver;
            _space.Drop();
        }

        private void BindView()
        {
            View.UI.Restart += OnRestart;
            _spaceship.Create += CreateSpaceship;
            _spaceship.Remove += RemoveSpaceship;
            _sources.Bind(View);
        }

        private void DropView()
        {
            View.UI.Restart -= OnRestart;
            _spaceship.Create -= CreateSpaceship;
            _spaceship.Remove -= RemoveSpaceship;
            _sources.Drop(View);
        }

        private void CreateSpaceship(ISpaceship spaceship)
        {
            View.CreateSpaceship(spaceship, _laser);
        }
        
        private void RemoveSpaceship(ISpaceship spaceship)
        {
            View.RemoveSpaceship(spaceship, _laser);
        }

        private void HandleInput()
        {
            _spaceship.ApplyRotation(
                _input.Rotate.ReadValue<float>() * _settings.Spaceship.Rotation * Time.deltaTime);

            _spaceship.ApplyThrust(
                _input.Thrust.ReadValue<float>() * _settings.Spaceship.Thrust * Time.deltaTime);

            if (_input.Laser.WasPressedThisFrame())
            {
                if (_space.TryApplyLaserShot(out var ray))
                {
                    View.CreateLaser(ray);
                }                
            }

            if (_input.Bullet.WasPressedThisFrame())
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

        private void OnGameOver() => View.UI.ShowPoints(_space.Points);

        private void OnRestart() => Restart?.Invoke();
    }
}