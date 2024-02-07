using System;
using System.Collections.Generic;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces.Base;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Custom
{
    public class BulletModel : ISource<Bullet>
    {
        public event Action<Bullet> Create;
        public event Action<Bullet> Remove;
        
        private readonly GameField _field;
        private readonly BulletSettings _settings;
        private readonly AsteroidModel _asteroids;

        private readonly List<Bullet> _bullets = new();

        private float _timerReload;

        public BulletModel(GameField field, BulletSettings settings, AsteroidModel asteroids)
        {
            _field = field;
            _settings = settings;
            _asteroids = asteroids;
        }

        public bool TryApplyShot(Vector2 position, Vector2 direction)
        {
            if (_timerReload < 0)
            {
                var bullet = new Bullet(position, direction, _settings.Lifetime);
                _bullets.Add(bullet);
                Create?.Invoke(bullet);
                
                _timerReload = _settings.Reload;

                return true;
            }

            return false;
        }

        private void UpdateMovement(float deltaTime)
        {
            foreach (var bullet in _bullets)
            {
                bullet.ApplyMovement(deltaTime, _field);
            }
        }

        private void UpdateLifetimeAndCollisions(float deltaTime)
        {
            for (var b = _bullets.Count - 1; b >= 0; b--)
            {
                var bullet = _bullets[b];
                
                if (bullet.ApplyLifetime(deltaTime))
                {
                    if (_asteroids.ApplyBullet(bullet.Position))
                    {
                        _bullets.RemoveAt(b);
                        Remove?.Invoke(bullet);
                    }
                }
                else
                {
                    _bullets.RemoveAt(b);
                    Remove?.Invoke(bullet);
                }
            }
        }

        public void Update(float deltaTime)
        {
            _timerReload -= deltaTime;

            UpdateMovement(deltaTime);
            UpdateLifetimeAndCollisions(deltaTime);
        }
    }
}