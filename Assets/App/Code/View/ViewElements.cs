using System;
using System.Collections.Generic;
using App.Code.Model.Binding;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces;
using App.Code.View.Custom;
using App.Code.View.Pool;
using App.Code.View.Tools;
using App.Code.View.UI.Dashboard;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Code.View
{
    public class ViewElements : MonoBehaviour
    {
        [SerializeField] private LaserView _laserPrefab;
        
        private ViewPool _pool;
        
        private LaserUI _laserUI;
        private SpaceshipUI _spaceshipUI;
        
        private readonly Dictionary<Asteroid, MonoView> _asteroids = new();
        private readonly Dictionary<Bullet, MonoView> _bullets = new();
        
        private SpaceshipView _spaceship;

        private void Awake()
        {
            if (!TryGetComponent(out _pool))
            {
                throw new InvalidOperationException($"Unable to find {typeof(ViewPool).FullName} component!");
            }

            _spaceshipUI = GetComponentInChildren<SpaceshipUI>() 
                           ?? throw new InvalidOperationException($"Unable to find {typeof(SpaceshipUI).FullName} component!");
            
            _laserUI = GetComponentInChildren<LaserUI>() 
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
            var view = _pool.Obtain(ElementType.Spaceship, spaceship.Position);
            _spaceship = view.gameObject.AddComponent<SpaceshipView>();
            _spaceship.Bind(spaceship);
        }

        public void RemoveSpaceship(ISpaceship spaceship)
        {
            _spaceship.Drop(spaceship);
            var view = _spaceship.gameObject.GetComponent<MonoView>();
            Object.Destroy(view);
            _pool.Release(view);
        }

        public void CreateAsteroid(Asteroid asteroid)
        {
            var view = _pool.Obtain(
                asteroid.IsFragment ? ElementType.Fragment : ElementType.Asteroid, 
                asteroid.Position);
            
            view.gameObject.AddComponent<PositionableView>().Bind(asteroid);
            _asteroids.Add(asteroid, view);
        }

        public void RemoveAsteroid(Asteroid asteroid)
        {
            var view = _asteroids[asteroid];
            var positionable = view.gameObject.GetComponent<PositionableView>();
            positionable.Drop(asteroid);
            Object.Destroy(positionable);
            _pool.Release(view);
            _asteroids.Remove(asteroid);
        }

        public void CreateBullet(Bullet bullet)
        {
            var view = _pool.Obtain(ElementType.Bullet, bullet.Position);
            view.gameObject.AddComponent<PositionableView>().Bind(bullet);
            _bullets.Add(bullet, view);
        }

        public void RemoveBullet(Bullet bullet)
        {
            var view = _bullets[bullet];
            var positionable = view.gameObject.GetComponent<PositionableView>();
            positionable.Drop(bullet);
            Object.Destroy(positionable);
            _pool.Release(view);
            _bullets.Remove(bullet);
        }
    }
}