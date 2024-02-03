using System;
using System.Collections.Generic;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Entities;
using App.Code.Model.Logical;
using App.Code.Model.Logical.Field;
using App.Code.Tools;
using UnityEngine;

namespace App.Code.Model
{
    public class OpenSpace : ISource
    {
        public event Action<IPositionable> ElementCreate;
        public event Action<IPositionable> ElementRemove;

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
            _spaceship = new Spaceship(_field.GetSpaceshipPosition(), Vector2.zero, _settings.ElementRadius);
            ElementCreate?.Invoke(_spaceship);
            
            foreach (var asteroid in _builder.BuildCollection(10))
            {
                _asteroids.Add(asteroid);
                ElementCreate?.Invoke(asteroid);
            }
        }

        public void ApplyDelta(float deltaTime)
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
    }
}