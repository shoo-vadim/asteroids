using System;
using System.Collections.Generic;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Entities;
using App.Code.Model.Logical;
using App.Code.Model.Logical.Extensions;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Custom.Asteroids
{
    public class AsteroidsModel : IElementSource
    {
        public event Action<IElement> ElementCreate;
        public event Action<IElement> ElementRemove;

        private readonly GameField _field;
        private readonly AsteroidsSettings _settings;
        private readonly AsteroidsBuilder _builder;

        private readonly List<Asteroid> _asteroids = new();

        private float _timerSpawn;

        public AsteroidsModel(GameField field, GameSettings settings)
        {
            _field = field;
            _settings = settings.Asteroids;
            _builder = new AsteroidsBuilder(field, settings.Asteroids.Speed);
        }
        
        private void AddAndNotify(Asteroid asteroid)
        {
            _asteroids.Add(asteroid);
            ElementCreate?.Invoke(asteroid);
        }

        public void Build(int count)
        {
            _timerSpawn = _settings.Spawn.GetRandom();

            foreach (var asteroid in _builder.BuildCollection(count))
            {
                AddAndNotify(asteroid);
            }
        }

        public bool ApplyBullet(Vector2 position)
        {
            for (var a = _asteroids.Count - 1; a >= 0; a--)
            {
                var asteroid = _asteroids[a];
                if (asteroid.HasIntersectionWithPoint(position))
                {
                    _asteroids.RemoveAt(a);
                    ElementRemove?.Invoke(asteroid);

                    if (!asteroid.IsFragment)
                    {
                        AddAndNotify(asteroid.CreateFragment(2));
                        AddAndNotify(asteroid.CreateFragment(2));
                    }

                    return true;
                }
            }

            return false;
        }

        private void UpdateSpawn(float deltaTime)
        {
            _timerSpawn -= deltaTime;

            if (_timerSpawn < 0)
            {
                AddAndNotify(_builder.BuildSingle());
                _timerSpawn = _settings.Spawn.GetRandom();
            }
        }

        private void UpdateMovement(float deltaTime)
        {
            foreach (var asteroid in _asteroids)
            {
                asteroid.ApplyMovement(deltaTime, _field);
            }
        }

        public void Update(float deltaTime)
        {
            UpdateSpawn(deltaTime);
            UpdateMovement(deltaTime);
        }
    }
}