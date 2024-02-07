using System;
using App.Code.Model.Custom;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model
{
    public class SpaceModel : ISpace
    {
        public event Action GameOver;

        public ISpaceship Spaceship => _spaceship;
        public ILaser Laser => _laser;

        private readonly GameField _field;
        private readonly GameSettings _settings;

        private readonly AsteroidsModel _asteroids;
        private readonly BulletsModel _bullets;
        
        private readonly LaserModel _laser;
        private Spaceship _spaceship;

        public SpaceModel(GameField field, GameSettings settings)
        {
            (_field, _settings) = (field, settings);

            _laser = new LaserModel(settings.Spaceship.Laser);
            _asteroids = new AsteroidsModel(field, settings);
            _bullets = new BulletsModel(field, _settings.Spaceship.Bullet, _asteroids);
        }
        
        public void Build(int asteroidsCount)
        {
            _spaceship = new Spaceship(_settings.Spaceship, Vector2.zero, _settings.ElementRadius);

            _asteroids.Build(asteroidsCount);
        }

        public bool TryApplyBulletShot()
        {
            return _bullets.TryApplyShot(
                _spaceship.Position + _spaceship.Direction, 
                _spaceship.Direction * _settings.Spaceship.Bullet.Speed);
        }

        public bool TryApplyLaserShot(out Ray ray)
        {
            if (!_laser.CanApplyShot)
            {
                ray = default;
                return false;
            }

            ray = default;
            _laser.ApplyShot();
            return true;
        }

        public void Update(float deltaTime)
        {
            _spaceship.ApplyMovement(deltaTime, _field);
            
            _asteroids.Update(deltaTime);
            _bullets.Update(deltaTime);

            if (_asteroids.HasAnyIntersection(_spaceship))
            {
                GameOver?.Invoke();
            }
        }
    }
}