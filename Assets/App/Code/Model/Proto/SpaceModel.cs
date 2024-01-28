using System;
using System.Linq;
using App.Code.Model.Proto.Field;
using App.Code.Tools;
using Random = UnityEngine.Random;

namespace App.Code.Model.Proto
{
    public class SpaceModel
    {
        public Entity[] Entities { get; }

        private readonly GameField _field;

        public SpaceModel(GameField field, Entity[] entities)
        {
            _field = field;
            Entities = entities;
        }

        public void AddEntity(Entity entity)
        {
            var index = Array.FindIndex(Entities, e => e == null);
            if (index < 0)
            {
                throw new OutOfMemoryException("Unable to create new entity");
            }

            Entities[index] = entity;
        }

        public void DestroyRandomEntity()
        {
            var available = Entities.GetNotEmptyIndexes().ToArray();
            var index = available[Random.Range(0, available.Length)];
            Entities[index] = null;
        }

        public void Update(float delta)
        {
            foreach (var entity in Entities)
            {
                if (entity == null)
                {
                    continue;
                }
                
                entity.Position += entity.Direction * (entity.Speed * delta);
                
                if (_field.GetMirroredPosition(entity.Position) is (true, var position))
                {
                    entity.Position = position;
                }
            }
        }
    }
}