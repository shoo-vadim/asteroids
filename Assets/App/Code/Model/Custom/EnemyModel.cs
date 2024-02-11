using System;
using App.Code.Model.Custom.Bullets;
using App.Code.Model.Entities;
using App.Code.Model.Entities.Base;
using App.Code.Model.Interfaces.Base;
using UnityEngine;

namespace App.Code.Model.Custom
{
    public class EnemyModel : ISource<Body>, IPointable
    {
        public event Action<Body> Create;
        public event Action<Body> Remove;

        public event Action Point;

        private readonly EnemyBulletModel _bullets;
        private readonly SpaceshipModel _spaceship;
        private Enemy _enemy;

        private float _time;

        public EnemyModel(EnemyBulletModel bullets, SpaceshipModel spaceship)
        {
            _bullets = bullets;
            _spaceship = spaceship;
        }

        public bool ApplyBullet(Vector2 position)
        {
            if (_enemy == null || !_enemy.HasIntersectionWithPoint(position))
            {
                return false;
            }

            var enemy = _enemy;
            _enemy = null;
            Remove?.Invoke(enemy);
            Point?.Invoke();
            return true;
        }

        public void Update(float deltaTime)
        {
            _time -= deltaTime;

            if (_time < 0)
            {
                // _bullets.Add(new Bullet(Vector2.zero, Vector2.up, 5f));
                _time = 2f;
            }
        }
    }
}