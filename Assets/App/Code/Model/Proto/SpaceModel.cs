using System;
using System.Linq;
using App.Code.Model.Binding;
using App.Code.Model.Entities;
using App.Code.Model.Logical;
using App.Code.Model.Logical.Field;
using App.Code.Model.Proto.Data;
using App.Code.Tools;
using App.Code.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Code.Model.Proto
{
    public class SpaceModel
    {
        public Spaceship Spaceship { get; private set; }

        public EntityLegacy[] Entities { get; }

        private readonly GameField _field;

        public SpaceModel(GameField field, EntityLegacy[] entities)
        {
            _field = field;
            Entities = entities;
        }

        public void DestroyIntersected(Vector2 position)
        {
            for (var i = 0; i < Entities.Length; i++)
            {
                if (Entities[i] == null)
                {
                    continue;
                }
                
                // if (Entities[i].HasIntersectionWithPoint(position))
                // {
                //     DestroyEntity(i);
                //     return;
                // }
            }
        }

        public void AddEntity(EntityLegacy entity)
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
            
            DestroyEntity(available[Random.Range(0, available.Length)]);
        }

        private void DestroyEntity(int index)
        {
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

        private static (EntityLegacy left, EntityLegacy right) CreateFragments(EntityLegacy source)
        {
            EntityLegacy CreateRotated(int degrees) => new()
            {
                ElementType = ElementType.Fragment,
                Movement = source.Movement.GetRotated(degrees) * 2,
                Position = source.Position,
            };
            
            return (CreateRotated(+20), CreateRotated(-20));
        } 
            
        public void Update(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                if (entity == null)
                {
                    continue;
                }
                
                entity.ApplyMovement(deltaTime);

                if (_field.GetMirroredPosition(entity.Position) is (true, var position))
                {
                    entity.Position = position;
                }
            }
        }
    }
}