using System;
using System.Collections.Generic;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces;
using App.Code.Model.Logical.Extensions;
using App.Code.View.Binding;
using App.Code.View.Binding.Custom;
using App.Code.View.Pool;
using App.Code.View.Tools;
using App.Code.View.UI.Dashboard;
using UnityEngine;

namespace App.Code.View.Elements
{
    public class GameElements : AsteroidElements
    {
        [SerializeField] private LaserView _laserPrefab;

        private LaserUI _laserUI;
        private SpaceshipUI _spaceshipUI;
        
        private readonly Dictionary<Bullet, PositionableView<Bullet>> _bullets = new();
        
        private SpaceshipView _spaceship;

        protected override void Awake()
        {
            base.Awake();

            _spaceshipUI = 
                GetComponentInChildren<SpaceshipUI>() 
                ?? throw new InvalidOperationException($"Unable to find {typeof(SpaceshipUI).FullName} component!");
            
            _laserUI = 
                GetComponentInChildren<LaserUI>() 
                ?? throw new InvalidOperationException($"Unable to find {typeof(LaserUI).FullName} component!");
        }

        public void BindUI(ISpaceship spaceship, ILaser laser)
        {
            _spaceshipUI.Bind(spaceship);
            _laserUI.Bind(laser);
        }

        public void DropUI(ISpaceship spaceship, ILaser laser)
        {
            _spaceshipUI.Drop(spaceship);
            _laserUI.Drop(laser);
        }

        public void CreateLaser(Ray2D ray)
        {
            var laser = Instantiate(_laserPrefab);
            laser.Setup(ray);
        }

        public void CreateSpaceship(ISpaceship spaceship)
        {
            _spaceship = ObtainElement<SpaceshipView, ISpaceship>(ElementType.Spaceship, spaceship);
        }

        public void RemoveSpaceship(ISpaceship spaceship)
        {
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


    }
}