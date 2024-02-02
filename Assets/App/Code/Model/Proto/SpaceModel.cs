using System;
using System.Linq;
using App.Code.Model.Proto.Field;
using App.Code.Tools;
using App.Code.View;
using UnityEngine;
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
            if (available.Length <= 0)
            {
                Debug.Log("All entities already destroyed!");
            }
            
            var index = available[Random.Range(0, available.Length)];

            switch (Entities[index].ElementType)
            {
                case ElementType.Fragment:
                    Entities[index] = null;
                    break;
                case ElementType.Asteroid:
                    var (left, right) = CreateFragments(Entities[index]);
                    Entities[index] = left; 
                    AddEntity(right);
                    break;
            }
        }

        private static (Entity left, Entity right) CreateFragments(Entity source)
        {
            Entity CreateRotated(int degrees) => new()
            {
                ElementType = ElementType.Fragment,
                Direction = source.Direction.GetRotated(degrees),
                Position = source.Position,
                Speed = source.Speed * 2
            };
            
            return (CreateRotated(+20), CreateRotated(-20));
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