using System;
using System.Collections.Generic;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Entities;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Custom
{
    public class BulletsModel : IElementSource
    {
        public event Action<IElement> ElementCreate;
        public event Action<IElement> ElementRemove;
        
        private readonly GameField _field;
        private readonly BulletSettings _settings;
        private readonly AsteroidsModel _asteroids;

        private readonly List<Bullet> _bullets = new();

        private float _timerReload;

        public BulletsModel(GameField field, BulletSettings settings, AsteroidsModel asteroids)
        {
            _field = field;
            _settings = settings;
            _asteroids = asteroids;
        }

        public void Create(Vector2 position, Vector2 direction)
        {
            if (_timerReload < 0)
            {
                var bullet = new Bullet(position, direction, _settings.Lifetime);
                _bullets.Add(bullet);
                ElementCreate?.Invoke(bullet);
                
                _timerReload = _settings.Reload;
            }
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
                        ElementRemove?.Invoke(bullet);
                    }
                }
                else
                {
                    _bullets.RemoveAt(b);
                    ElementRemove?.Invoke(bullet);
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