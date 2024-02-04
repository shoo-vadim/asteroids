using System.Collections.Generic;
using App.Code.Model.Entities;
using App.Code.Model.Logical.Extensions;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Logical
{
    public class AsteroidsBuilder
    {
        private readonly GameField _field;
        private readonly Range<float> _speed;
        
        public AsteroidsBuilder(GameField field, Range<float> speed)
        {
            _field = field;
            _speed = speed;
        }

        private Vector2 GetRandomMovement() =>
            Vector2.up.GetRotated(Random.Range(0, 360)) * Random.Range(_speed.Min, _speed.Max); 

        public Asteroid BuildSingle()
        {
            return new Asteroid(
                _field.GetRandomPositionOnBorder(),
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