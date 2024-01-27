using UnityEngine;

namespace App.Code.Model.Proto
{
    public class OpenSpace
    {
        private readonly Field _field;
        private readonly Entity[] _entities;

        public OpenSpace(Field field, Entity[] entities)
        {
            _field = field;
            _entities = entities;
        }

        public void Update(float delta)
        {
            foreach (var entity in _entities)
            {
                entity.Position += entity.Direction * entity.Speed * delta;
                
                if (_field.GetMirroredPosition(entity.Position) is (true, var position))
                {
                    entity.Position = position;
                }
                
                Debug.DrawRay(entity.Position, entity.Direction);
            }
        }
    }
}