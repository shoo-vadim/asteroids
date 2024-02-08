using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Entities;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Custom.Bullets
{
    public class PlayerBulletModel : BulletModel
    {
        private readonly BulletSettings _settings;
        private readonly AsteroidModel _asteroids;
        
        private float _timerReload;
        
        public PlayerBulletModel(GameField field, BulletSettings settings, AsteroidModel asteroids) : base(field)
        {
            _settings = settings;
            _asteroids = asteroids;
        }
        
        public bool TryApplyShot(Vector2 position, Vector2 direction)
        {
            if (_timerReload <= 0)
            {
                Add(new Bullet(position, direction, _settings.Lifetime));
                _timerReload = _settings.Reload;
                return true;
            }

            return false;
        }

        protected override bool ApplyBulletLifetime(Bullet bullet)
        {
            return _asteroids.ApplyBullet(bullet.Position);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _timerReload -= deltaTime;
        }
    }
}