using System;
using System.Collections.Generic;
using App.Code.Model.Binding;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Entities;
using App.Code.Model.Logical;
using App.Code.Model.Logical.Extensions;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model
{
    public class OpenSpace : ISource
    {
        public event Action<ElementType, IElement> ElementCreate;
        public event Action<IElement> ElementRemove;

        public Spaceship Spaceship => _spaceship;

        private readonly GameField _field;
        private readonly GameSettings _settings;
        private readonly AsteroidsBuilder _builder;
        
        private readonly List<Asteroid> _asteroids = new();
        private readonly List<Bullet> _bullets = new();
        
        private Spaceship _spaceship;

        public OpenSpace(GameField field, GameSettings settings)
        {
            (_field, _settings) = (field, settings);
            _builder = new AsteroidsBuilder(_field, _settings.AsteroidsSpeed);
        }
        
        public void BuildWorld()
        {
            _spaceship = new Spaceship(_settings.Spaceship, Vector2.zero, _settings.ElementRadius);
            ElementCreate?.Invoke(ElementType.Spaceship, _spaceship);
            
            foreach (var asteroid in _builder.BuildCollection(10))
            {
                _asteroids.Add(asteroid);
                ElementCreate?.Invoke(ElementType.Asteroid, asteroid);
            }
        }

        public void ApplyShot()
        {
            var bullet = new Bullet(
                _spaceship.Position + _spaceship.Direction, 
                _spaceship.Direction * _settings.Spaceship.Bullet.Speed,
                _settings.Spaceship.Bullet.Lifetime);
            
            _bullets.Add(bullet);
            ElementCreate?.Invoke(ElementType.Bullet, bullet);
        }

        private void ApplyMovement(float deltaTime)
        {
            _spaceship.ApplyMovement(deltaTime, _field);

            foreach (var bullet in _bullets)
            {
                bullet.ApplyMovement(deltaTime, _field);
            }
            
            foreach (var asteroid in _asteroids)
            {
                asteroid.ApplyMovement(deltaTime, _field);
            }
        }

        private void CreateFragment(Asteroid asteroid)
        {
            var fragment = asteroid.CreateFragment(2);
            _asteroids.Add(fragment);
            ElementCreate?.Invoke(ElementType.Fragment, fragment);
        }

        private void ApplyBulletsLifetime(float deltaTime)
        {
            for (var b = _bullets.Count - 1; b >= 0; b--)
            {
                var bullet = _bullets[b];
                
                if (!bullet.ApplyLifetime(deltaTime))
                {
                    _bullets.RemoveAt(b);
                    ElementRemove?.Invoke(bullet);
                    continue;
                }

                for (var a = _asteroids.Count - 1; a >= 0; a--)
                {
                    var asteroid = _asteroids[a];
                    if (asteroid.HasIntersectionWithPoint(bullet.Position))
                    {
                        _asteroids.RemoveAt(a);
                        ElementRemove?.Invoke(asteroid);
                        _bullets.RemoveAt(b);
                        ElementRemove?.Invoke(bullet);

                        if (!asteroid.IsFragment)
                        {
                            CreateFragment(asteroid);
                            CreateFragment(asteroid);
                        }
                        
                        break;
                    }
                }
            }
        }

        public void ApplyDelta(float deltaTime)
        {
            ApplyMovement(deltaTime);
            ApplyBulletsLifetime(deltaTime);
        }
    }
}