using System;
using App.Code.Model.Custom.Bullets;
using App.Code.Model.Entities;
using App.Code.Model.Entities.Base;
using App.Code.Model.Interfaces.Base;
using UnityEngine;

namespace App.Code.Model.Custom
{
    public class EnemyModel : ISource<Body>
    {
        public event Action<Body> Create;
        public event Action<Body> Remove;

        private readonly EnemyBulletModel _bullets;

        private float _time;

        public EnemyModel(EnemyBulletModel bullets)
        {
            _bullets = bullets;
        }

        public void Update(float deltaTime)
        {
            _time -= deltaTime;

            if (_time < 0)
            {
                _bullets.Add(new Bullet(Vector2.zero, Vector2.up, 5f));
                _time = 2f;
            }
            _bullets.Update(deltaTime);
        }
    }
}