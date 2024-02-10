using System;
using System.Collections.Generic;
using App.Code.Model.Entities;
using App.Code.Model.Entities.Base;
using App.Code.Model.Interfaces.Base;
using App.Code.Model.Logical.Extensions;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Custom.Asteroids
{
    public class AsteroidModel : ISource<Asteroid>
    {
        public event Action<Asteroid> Create;
        public event Action<Asteroid> Remove;
        
        private readonly GameField _field;
        private readonly AsteroidSettings _settings;
        private readonly AsteroidBuilder _builder;

        private readonly List<Asteroid> _asteroids;

        private float _timerSpawn;

        public AsteroidModel(GameField field,
            AsteroidSettings settings, 
            AsteroidBuilder builder, 
            IEnumerable<Asteroid> asteroids)
        {
            _field = field;
            _settings = settings;
            _builder = builder;
            _asteroids = new List<Asteroid>(asteroids);
            _timerSpawn = settings.Spawn.GetRandom();
        }
        
        private void AddAndNotify(Asteroid asteroid)
        {
            _asteroids.Add(asteroid);
            Create?.Invoke(asteroid);
        }

        private void Break(Asteroid asteroid)
        {
            _asteroids.Remove(asteroid);
            Remove?.Invoke(asteroid);

            if (!asteroid.IsFragment)
            {
                AddAndNotify(asteroid.CreateFragment(2));
                AddAndNotify(asteroid.CreateFragment(2));
            }
        }

        public void ApplyLaser(Ray2D ray)
        {
            for (var a = _asteroids.Count - 1; a >= 0; a--)
            {
                var asteroid = _asteroids[a];
                if (asteroid.HasIntersectionWithRay(ray))
                {
                    Break(asteroid);
                }
            }            
        }

        public bool ApplyBullet(Vector2 position)
        {
            for (var a = _asteroids.Count - 1; a >= 0; a--)
            {
                var asteroid = _asteroids[a];
                if (asteroid.HasIntersectionWithPoint(position))
                {
                    Break(asteroid);
                    return true;
                }
            }

            return false;
        }

        public bool HasAnyIntersection(Body body)
        {
            foreach (var t in _asteroids)
            {
                if (t.HasIntersectionWithBody(body))
                {
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