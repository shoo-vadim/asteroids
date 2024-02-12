using System;
using System.Collections.Generic;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces;
using App.Code.Model.Logical.Extensions;
using App.Code.View.Binding;
using App.Code.View.Binding.Custom;
using App.Code.View.Pool;
using App.Code.View.Tools;
using App.Code.View.UI;
using App.Code.View.UI.Dashboard;
using UnityEngine;

namespace App.Code.View.Elements
{
    public class GameElements : AsteroidElements
    {
        [SerializeField] private LaserView _laserPrefab;

        public GameUI UI { get; private set; }
        
        private readonly Dictionary<Bullet, PositionableView<Bullet>> _bullets = new();
        private readonly Dictionary<Enemy, PositionableView<Enemy>> _enemies = new();
        
        private SpaceshipView _spaceship;

        protected override void Awake()
        {
            base.Awake();
            
            UI = GetComponent<GameUI>() 
                  ?? throw new InvalidOperationException($"Unable to find {typeof(SpaceshipUI).FullName} component!");
        }
        
        public void CreateLaser(Ray2D ray)
        {
            var laser = Instantiate(_laserPrefab);
            laser.Setup(ray);
        }

        public void CreateSpaceship(ISpaceship spaceship, ILaser laser)
        {
            UI.Laser.Bind(laser);
            UI.Spaceship.Bind(spaceship);
            _spaceship = ObtainElement<SpaceshipView, ISpaceship>(ElementType.Spaceship, spaceship);
        }

        public void RemoveSpaceship(ISpaceship spaceship, ILaser laser)
        {
            UI.Laser.Drop(laser);
            UI.Spaceship.Drop(spaceship);
            ReleaseElement(_spaceship, spaceship);
        }
        
        public void CreateBullet(Bullet bullet)
        {
            _bullets.Add(bullet, ObtainElement<BulletView, Bullet>(ElementType.Bullet, bullet));
        }

        public void RemoveBullet(Bullet bullet)
        {
            ReleaseElement(_bullets.GetAndRemove(bullet), bullet);
        }
        
        public void CreateEnemy(Enemy enemy)
        {
            _enemies.Add(enemy, ObtainElement<EnemyView, Enemy>(ElementType.Aliens, enemy));
        }

        public void RemoveEnemy(Enemy enemy)
        {
            ReleaseElement(_enemies.GetAndRemove(enemy), enemy);
        }
    }
}