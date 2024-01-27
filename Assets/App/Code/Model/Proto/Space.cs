using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Code.Model.Proto
{
    public class Space
    {
        private readonly Field _field = new(10, 5);
        
        private readonly Entity[] _entities;

        public Space()
        {
            _entities = CreateEntities(10).ToArray();
            
            if (_field.GetMirroredPosition(Vector2.zero) is (true, var position))
            {
                
            }
        }

        public void Update()
        {
            
        }

        private IEnumerable<Entity> CreateEntities(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new Entity
                {
                    Position = _field.GetRandomPosition()
                };
            }
        }
    }
}