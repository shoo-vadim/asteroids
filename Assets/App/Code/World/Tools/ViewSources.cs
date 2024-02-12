using App.Code.Model.Entities;
using App.Code.Model.Interfaces.Base;
using App.Code.View.Elements;

namespace App.Code.World.Tools
{
    public class ViewSources
    {
        private readonly ISource<Asteroid> _asteroids;
        private readonly ISource<Enemy> _enemies;
        private readonly ISource<Bullet> _bulletsPlayer;
        private readonly ISource<Bullet> _bulletsEnemy;

        public ViewSources(ISource<Asteroid> asteroids, ISource<Enemy> enemies, ISource<Bullet> bulletsPlayer, ISource<Bullet> bulletsEnemy)
        {
            _asteroids = asteroids;
            _enemies = enemies;
            _bulletsPlayer = bulletsPlayer;
            _bulletsEnemy = bulletsEnemy;
        }

        public void Bind(GameElements view)
        {
            _enemies.Create += view.CreateEnemy;
            _enemies.Remove += view.RemoveEnemy;
            _asteroids.Create += view.CreateAsteroid;
            _asteroids.Remove += view.RemoveAsteroid;
            _bulletsPlayer.Create += view.CreateBullet;
            _bulletsPlayer.Remove += view.RemoveBullet;
            _bulletsEnemy.Create += view.CreateBullet;
            _bulletsEnemy.Remove += view.RemoveBullet;
        }

        public void Drop(GameElements view)
        {
            _enemies.Create -= view.CreateEnemy;
            _enemies.Remove -= view.RemoveEnemy;
            _asteroids.Create -= view.CreateAsteroid;
            _asteroids.Remove -= view.RemoveAsteroid;
            _bulletsPlayer.Create -= view.CreateBullet;
            _bulletsPlayer.Remove -= view.RemoveBullet;
            _bulletsEnemy.Create -= view.CreateBullet;
            _bulletsEnemy.Remove -= view.RemoveBullet;
        }
    }
}