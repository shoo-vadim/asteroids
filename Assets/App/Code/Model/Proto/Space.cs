using System.Collections.Generic;
using System.Linq;

namespace App.Code.Model.Proto
{
    public class Space
    {
        private readonly Field _field = new((-10, +10), (-5, +5));
        private readonly Entity[] _entities;

        public Space()
        {
            _entities = CreateEntities(10).ToArray();
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