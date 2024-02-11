using App.Code.Model.Entities;

namespace App.Code.Model.Custom.Enemies.State
{
    public sealed class EnemyRunning : EnemyState
    {
        public Enemy Enemy { get; }

        private readonly float _reload;
        private float _current;

        public EnemyRunning(Enemy enemy, float reload)
        {
            Enemy = enemy;
            _reload = reload;
        }

        public bool TryShoot(float deltaTime)
        {
            if ((_current -= deltaTime) < 0f)
            {
                _current = _reload;
                return true;
            }

            return false;
        }
    }
}