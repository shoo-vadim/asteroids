using System.Collections.Generic;
using System.Linq;
using App.Code.Model.Proto;
using App.Code.View;
using App.Code.View.Entities;
using App.Code.View.Source;
using UnityEngine;

namespace App.Code
{
    [RequireComponent(typeof(ViewSource))]
    public class MonoWorld : MonoBehaviour
    {
        private SpaceModel _space;
        private MonoView[] _views;
        
        private void Awake()
        {
            var field = new Field(30, 15);
            var entities = CreateRandomEntities(field, 10).ToArray();
            _views = CreateViewsFromEntities(GetComponent<ViewSource>(), entities).ToArray();
            _space = new SpaceModel(field, entities);
        }

        private void Update()
        {
            _space.Update(Time.deltaTime);
            
            RefreshViews();
        }

        private void RefreshViews()
        {
            for (var i = 0; i < _space.Entities.Length; i++)
            {
                _views[i].Refresh(_space.Entities[i].Position);
            }
        }

        private static IEnumerable<MonoView> CreateViewsFromEntities(ViewSource source, IEnumerable<Entity> entities)
        {
            return entities.Select(t => source.Create<AsteroidView>(t.Position));
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