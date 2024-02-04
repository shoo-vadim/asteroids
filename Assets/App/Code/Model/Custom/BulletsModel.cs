using System;
using System.Collections.Generic;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Entities;
using App.Code.Model.Logical.Field;
using UnityEngine;

namespace App.Code.Model.Custom
{
    public class BulletsModel : IElementSource
    {
        public event Action<IElement> ElementCreate;
        public event Action<IElement> ElementRemove;
        
        private readonly GameField _field;
        private readonly AsteroidsModel _asteroids;
        private readonly float _lifetime;
        
        private readonly List<Bullet> _bullets = new();

        public BulletsModel(GameField field, AsteroidsModel asteroids, float lifetime)
        {
            _field = field;
            _asteroids = asteroids;
            _lifetime = lifetime;
        }

        public void Create(Vector2 position, Vector2 direction)
        {
            var bullet = new Bullet(position, direction, _lifetime);
            _bullets.Add(bullet);
            ElementCreate?.Invoke(bullet);
        }

        public void Update(float deltaTime)
        {
            foreach (var bullet in _bullets)
            {
                bullet.ApplyMovement(deltaTime, _field);
            }
            
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
    }
}