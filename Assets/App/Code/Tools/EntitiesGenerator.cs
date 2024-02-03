using UnityEngine;
using System.Collections.Generic;
using App.Code.Model.Binding;
using App.Code.Model.Logical;
using App.Code.Model.Logical.Field;
using App.Code.Model.Proto;
using App.Code.Model.Proto.Data;
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

        public EntityLegacy CreateRandomSingleEntity()
        {
            return new EntityLegacy
            {
                ElementType = ElementType.Asteroid,
                Position = _field.GetRandomPositionOnBorder(),
                Movement = Vector2.up.GetRotated(Random.Range(0, 360)) * Random.Range(_speed.Min, _speed.Max),
            };
        }

        public IEnumerable<EntityLegacy> CreateRandomEntities(int count, int total)
        {
            for (var i = 0; i < total; i++)
            {
                yield return i >= count ? null : CreateRandomSingleEntity();
            }
        }
    }
}