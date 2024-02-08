using System;
using App.Code.Model.Entities;
using App.Code.Model.Logical.Field;

namespace App.Code.Model.Custom.Bullets
{
    public class EnemyBulletModel : BulletModel
    {
        public event Action GameOver;

        private readonly Spaceship _spaceship;
        
        public EnemyBulletModel(GameField field, Spaceship spaceship) : base(field)
        {
            _spaceship = spaceship;
        }

        protected override bool ApplyBulletLifetime(Bullet bullet)
        {
            if (_spaceship.HasIntersectionWithPoint(bullet.Position))
            {
                GameOver?.Invoke();
                return true;
            }

            return false;
        }
    }
}