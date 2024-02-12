using App.Code.Model.Entities;
using App.Code.Model.Logical.Field;

namespace App.Code.Model.Logic.Bullets
{
    public class EnemyBulletModel : BulletModel
    {
        private readonly SpaceshipModel _spaceship;
        
        public EnemyBulletModel(GameField field, SpaceshipModel spaceship) : base(field)
        {
            _spaceship = spaceship;
        }

        protected override bool ApplyBulletLifetime(Bullet bullet)
        {
            return _spaceship.ApplyBullet(bullet.Position);
        }
    }
}