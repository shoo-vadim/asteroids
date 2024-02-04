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
        private readonly AsteroidsBuilder _builder;
        
        private readonly List<Asteroid> _asteroids = new();

        public AsteroidsModel(GameField field, GameSettings settings)
        {
            _field = field;
            _builder = new AsteroidsBuilder(field, settings.Asteroids.Speed);
        }

        public void Build(int count)
        {
            foreach (var asteroid in _builder.BuildCollection(count))
            {
                _asteroids.Add(asteroid);
                ElementCreate?.Invoke(asteroid);
            }
        }
        
        private void CreateFragment(Asteroid asteroid)
        {
            var fragment = asteroid.CreateFragment(2);
            _asteroids.Add(fragment);
            ElementCreate?.Invoke(fragment);
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
                        CreateFragment(asteroid);
                        CreateFragment(asteroid);
                    }

                    return true;
                }
            }

            return false;
        }

        public void Update(float deltaTime)
        {
            foreach (var asteroid in _asteroids)
            {
                asteroid.ApplyMovement(deltaTime, _field);
            }
        }
    }
}