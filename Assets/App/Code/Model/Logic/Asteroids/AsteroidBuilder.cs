using System;
using System.Collections.Generic;
using App.Code.Model.Entities;
using App.Code.Model.Logical.Extensions;
using App.Code.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Code.Model.Logic.Asteroids
{
    public class AsteroidBuilder
    {
        private readonly Func<Vector2> _factoryPosition;
        private readonly Range<float> _speed;
        
        public AsteroidBuilder(Func<Vector2> factoryPosition, Range<float> speed)
        {
            _factoryPosition = factoryPosition;
            _speed = speed;
        }

        private Vector2 GetRandomMovement() =>
            Vector2.up.GetRotated(Random.Range(0, 360)) * Random.Range(_speed.Min, _speed.Max); 

        public Asteroid BuildSingle()
        {
            return new Asteroid(
                _factoryPosition.Invoke(),
                GetRandomMovement(),
                1f,
                false
            );
        }

        public IEnumerable<Asteroid> BuildCollection(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return BuildSingle();
            }
        }
    }
}