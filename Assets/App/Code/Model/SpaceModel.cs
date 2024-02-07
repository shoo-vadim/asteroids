using System;
using App.Code.Model.Custom;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces;
using App.Code.Model.Interfaces.Base;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model
{
    public class SpaceModel : ISpace
    {
        public event Action GameOver;

        // public ISource<Asteroid> Asteroids => _asteroids;
        public ISource<Bullet> Bullets => _bullets;
        // public ISpaceship Spaceship => _spaceship;
        public ILaser Laser => _laser;

        private readonly GameField _field;
        private readonly GameSettings _settings;
        
        private readonly LaserModel _laser;
        private readonly Spaceship _spaceship;
        private readonly BulletModel _bullets;
        private readonly AsteroidModel _asteroids;

        public SpaceModel(
            GameField field, 
            GameSettings settings, 
            Spaceship spaceship, 
            LaserModel laser, 
            AsteroidModel asteroids,
            BulletModel bullets)
        {
            _field = field;
            _settings = settings;
            _spaceship = spaceship;
            _laser = laser;
            _asteroids = asteroids;
            _bullets = bullets;
        }
        
        public void Build(int asteroidsCount)
        {
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