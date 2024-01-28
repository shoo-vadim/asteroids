using UnityEngine;

namespace App.Code.Model.Proto
{
    public class SpaceModel
    {
        public Entity[] Entities { get; }

        private readonly Field _field;

        public SpaceModel(Field field, Entity[] entities)
        {
            _field = field;
            Entities = entities;
        }

        public void CreateRandomEntity()
        {
            
        }

        public void RemoveRandomEntity()
        {
            
        }

        public void Update(float delta)
        {
            foreach (var entity in Entities)
            {
                entity.Position += entity.Direction * (entity.Speed * delta);
                
                if (_field.GetMirroredPosition(entity.Position) is (true, var position))
                {
                    entity.Position = position;
                }
                
                Debug.DrawRay(entity.Position, entity.Direction);
            }
        }
    }
}