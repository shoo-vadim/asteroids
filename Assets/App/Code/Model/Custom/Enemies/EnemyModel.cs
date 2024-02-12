using System;
using App.Code.Model.Custom.Bullets;
using App.Code.Model.Custom.Enemies.State;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces.Base;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Custom.Enemies
{
    public class EnemyModel : ISource<Enemy>, IPointable
    {
        public event Action<Enemy> Create;
        public event Action<Enemy> Remove;

        public event Action Point;

        private readonly GameField _field;
        private readonly EnemyBulletModel _bullets;
        private readonly SpaceshipModel _spaceship;
        private readonly EnemySettings _settings;

        private EnemyState _state;

        public EnemyModel(GameField field, EnemyBulletModel bullets, SpaceshipModel spaceship, EnemySettings settings)
        {
            _field = field;
            _bullets = bullets;
            _spaceship = spaceship;
            _settings = settings;
            _state = new EnemyWaiting(settings.Spawn);
        }

        public bool ApplyPlayerBullet(Vector2 position)
        {
            if (_state is EnemyRunning running 
                && running.Enemy.HasIntersectionWithPoint(position))
            {
                Kill(running);
                return true;
            }

            return false;
        }

        public void ApplyLaser(Ray2D ray)
        {
            if (_state is EnemyRunning running && running.Enemy.HasIntersectionWithRay(ray))
            {
                Kill(running);
            }
        }

        private void Kill(EnemyRunning state)
        {
            var enemy = state.Enemy;
            Remove?.Invoke(enemy);
            Point?.Invoke();
            _state = new EnemyWaiting(_settings.Spawn);
        }

        private void UpdateWaiting(EnemyWaiting waiting, float deltaTime)
        {
            if (waiting.IsDone(deltaTime))
            {
                var enemy = new Enemy(_field.GetRandomPositionOnBorder(), _settings.Speed, _settings.Radius);
                Create?.Invoke(enemy);
                _state = new EnemyRunning(enemy, _settings.Bullet.Reload);
            }
        }

        private void UpdateRunning(EnemyRunning running, float deltaTime)
        {
            var enemy = running.Enemy;
            if (_spaceship.TryGetPosition(out var position))
            {
                enemy.ApplyMovementTowards(position, deltaTime, _field);
            }
            
            if (_spaceship.ApplyBody(enemy.Position, enemy.Radius))
            {
                Remove?.Invoke(enemy);
                return;
            }

            if (running.TryShoot(deltaTime))
            {
                var direction = (position - enemy.Position).normalized;
                _bullets.Add(new Bullet(
                    enemy.Position + direction * enemy.Radius, 
                    direction * _settings.Bullet.Speed, 
                    _settings.Bullet.Lifetime));
            }
        }

        public void Update(float deltaTime)
        {
            _bullets.Update(deltaTime);
            
            if (!_spaceship.IsAlive())
            {
                return;
            }
            
            switch (_state)
            {
                case EnemyWaiting waiting:
                    UpdateWaiting(waiting, deltaTime);
                    break;
                case EnemyRunning running:
                    UpdateRunning(running, deltaTime);
                    break;
            }
        }
    }
}