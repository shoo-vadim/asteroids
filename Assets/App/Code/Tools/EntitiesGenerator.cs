using UnityEngine;
using System.Collections.Generic;
using App.Code.Model.Proto;
using App.Code.Model.Proto.Data;
using App.Code.Model.Proto.Field;
using App.Code.View;


namespace App.Code.Tools
{
    public class EntitiesGenerator
    {
        private readonly GameField _field;
        private readonly Range<float> _speed;

        public EntitiesGenerator(GameField field, Range<float> speed)
        {
            _field = field;
            _speed = speed;
        }

        public Entity CreateRandomSingleEntity()
        {
            return new Entity
            {
                ElementType = ElementType.Asteroid,
                Position = _field.GetRandomPosition(),
                Direction = Vector2.up.GetRotated(Random.Range(0, 360)),
                Speed = Random.Range(_speed.Min, _speed.Max)
            };
        }

        public IEnumerable<Entity> CreateRandomEntities(int count, int total)
        {
            for (var i = 0; i < total; i++)
            {
                yield return i >= count ? null : CreateRandomSingleEntity();
            }
        }
    }
}