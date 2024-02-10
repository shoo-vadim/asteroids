using System;
using System.Collections.Generic;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces;
using App.Code.Model.Interfaces.Base;
using App.Code.Model.Logical.Extensions;
using App.Code.View.Binding;
using App.Code.View.Binding.Custom;
using App.Code.View.Custom;
using App.Code.View.Pool;
using App.Code.View.Tools;
using App.Code.View.UI.Dashboard;
using UnityEngine;

namespace App.Code.View
{
    public class ViewElements : MonoBehaviour
    {
        [SerializeField] private LaserView _laserPrefab;
        
        private ViewPool _pool;
        
        private LaserUI _laserUI;
        private SpaceshipUI _spaceshipUI;
        
        private readonly Dictionary<Asteroid, PositionableView<Asteroid>> _asteroids = new();
        private readonly Dictionary<Bullet, PositionableView<Bullet>> _bullets = new();
        
        private SpaceshipView _spaceship;

        private void Awake()
        {
            if (!TryGetComponent(out _pool))
            {
                throw new InvalidOperationException($"Unable to find {typeof(ViewPool).FullName} component!");
            }

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

        public void CreateAsteroid(Asteroid asteroid)
        {
            _asteroids.Add(asteroid, ObtainElement<AsteroidView, Asteroid>(
                asteroid.IsFragment ? ElementType.Fragment : ElementType.Asteroid, asteroid));
        }

        public void RemoveAsteroid(Asteroid asteroid)
        {
            ReleaseElement(_asteroids.GetAndRemove(asteroid), asteroid);
        }

        public void CreateBullet(Bullet bullet)
        {
            _bullets.Add(bullet, ObtainElement<BulletView, Bullet>(ElementType.Bullet, bullet));
        }

        public void RemoveBullet(Bullet bullet)
        {
            ReleaseElement(_bullets.GetAndRemove(bullet), bullet);
        }

        private TBind ObtainElement<TBind, TModel>(ElementType elementType, TModel model) 
            where TBind : BindableView<TModel>
            where TModel : IPositionable
        {
            var view = _pool.Obtain(elementType, model.Position);
            var bind = view.gameObject.AddComponent<TBind>();
            bind.Bind(model);
            return bind;
        }

        private void ReleaseElement<TBind, TModel>(TBind bind, TModel model)
            where TBind : BindableView<TModel>
            where TModel : IPositionable
        {
            bind.Drop(model);
            var view = bind.gameObject.GetComponent<TBind>();
            Destroy(bind);
            _pool.Release(view.GetComponent<MonoView>());
        }
    }
}