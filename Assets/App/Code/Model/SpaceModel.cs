using System;
using App.Code.Model.Custom;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Custom.Bullets;
using App.Code.Model.Custom.Enemies;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model
{
    public class SpaceModel
    {
        public event Action GameOver;
        
        public int Points { get; private set; }
        
        private readonly GameSettings _settings;
        
        private readonly LaserModel _laser;
        private readonly EnemyModel _enemy;
        private readonly SpaceshipModel _spaceship;
        private readonly PlayerBulletModel _bullets;
        private readonly AsteroidModel _asteroids;

        public SpaceModel(
            GameSettings settings, 
            SpaceshipModel spaceship, 
            LaserModel laser, 
            AsteroidModel asteroids,
            PlayerBulletModel bullets,
            EnemyModel enemy)
        {
            _settings = settings;
            _spaceship = spaceship;
            _enemy = enemy;
            _laser = laser;
            _asteroids = asteroids;
            _bullets = bullets;
        }

        public void Bind()
        {
            _enemy.Point += OnPoint;
            _asteroids.Point += OnPoint;
        }

        public void Drop()
        {
            _enemy.Point -= OnPoint;
            _asteroids.Point -= OnPoint;
        }
        
        public bool TryApplyBulletShot()
        {
            return _spaceship.TryGetShoot(out var point, out var direction) 
                   && _bullets.TryApplyShot(point, direction * _settings.Spaceship.Bullet.Speed);
        }

        public bool TryApplyLaserShot(out Ray2D ray)
        {
            if (!_laser.CanApplyShot || !_spaceship.TryGetShoot(out var point, out var direction))
            {
                ray = default;
                return false;
            }
            _asteroids.ApplyLaser(ray = new Ray2D(point, direction));
            _laser.ApplyShot();
            return true;
        }

        public void Update(float deltaTime)
        {
            _spaceship.Update(deltaTime);
            _asteroids.Update(deltaTime);
            _bullets.Update(deltaTime);
            _laser.Update(deltaTime);
            _enemy.Update(deltaTime);
        }

        private void OnPoint() => Points++;
    }
}