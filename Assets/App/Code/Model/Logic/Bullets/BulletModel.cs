using System;
using System.Collections.Generic;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces.Base;
using App.Code.Model.Logical.Field;

namespace App.Code.Model.Logic.Bullets
{
    public abstract class BulletModel : ISource<Bullet>
    {
        public event Action<Bullet> Create;
        public event Action<Bullet> Remove;
        
        private readonly GameField _field;

        private readonly List<Bullet> _bullets = new();

        protected BulletModel(GameField field)
        {
            _field = field;
        }

        private void UpdateMovement(float deltaTime)
        {
            foreach (var bullet in _bullets)
            {
                bullet.ApplyMovement(deltaTime, _field);
            }
        }
        
        private void UpdateLifetimeAndCollisions(float deltaTime)
        {
            for (var b = _bullets.Count - 1; b >= 0; b--)
            {
                var bullet = _bullets[b];
                
                if (bullet.ApplyLifetime(deltaTime))
                {
                    if (ApplyBulletLifetime(bullet))
                    {
                        _bullets.RemoveAt(b);
                        Remove?.Invoke(bullet);                        
                    }
                }
                else
                {
                    _bullets.RemoveAt(b);
                    Remove?.Invoke(bullet);
                }
            }
        }

        public void Add(Bullet bullet)
        {
            _bullets.Add(bullet);
            Create?.Invoke(bullet);
        }
        
        protected abstract bool ApplyBulletLifetime(Bullet bullet);

        public virtual void Update(float deltaTime)
        {
            UpdateMovement(deltaTime);
            UpdateLifetimeAndCollisions(deltaTime);
        }
    }
}