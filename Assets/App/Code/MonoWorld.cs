using System.Collections.Generic;
using System.Linq;
using App.Code.Model.Proto;
using UnityEngine;

namespace App.Code
{
    public class MonoWorld : MonoBehaviour
    {
        private OpenSpace _space;
        
        private void Awake()
        {
            var field = new Field(10, 5);
            _space = new OpenSpace(field, CreateRandomEntities(field, 10).ToArray());
        }

        private void Update()
        {
            _space.Update(Time.deltaTime);
        }
        
        private static IEnumerable<Entity> CreateRandomEntities(Field field, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new Entity
                {
                    Position = field.GetRandomPosition(),
                    Direction = Vector2.up.GetRotated(Random.Range(0, 360)),
                    Speed = Random.Range(3, 5)
                };
            }
        }
    }
}