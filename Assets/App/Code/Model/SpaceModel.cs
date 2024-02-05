using System;
using App.Code.Model.Binding.Interfaces;
using App.Code.Model.Custom;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Entities;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model
{
    public interface ISpaceModel : IElementSource
    {
        public event Action GameOver;
        
        public void ApplyShot();
        public void ApplyRotation(float degrees);
        public void ApplyThrust(float force);
        
        public void Build(int asteroidsCount);
        public void Update(float deltaTime);
    }
    
    public class SpaceModel : ISpaceModel
    {
        public event Action GameOver;
        public event Action<IElement> ElementCreate;
        public event Action<IElement> ElementRemove;
        
        private readonly GameField _field;
        private readonly GameSettings _settings;

        private readonly AsteroidsModel _asteroids;
        private readonly BulletsModel _bullets;

        private Spaceship _spaceship;

        public SpaceModel(GameField field, GameSettings settings)
        {
            (_field, _settings) = (field, settings);
            
            _asteroids = new AsteroidsModel(field, settings);
            _asteroids.ElementCreate += OnElementCreate;
            _asteroids.ElementRemove += OnElementRemove;

            _bullets = new BulletsModel(field, _settings.Spaceship.Bullet, _asteroids);
            _bullets.ElementCreate += OnElementCreate;
            _bullets.ElementRemove += OnElementRemove;
        }
        
        public void Build(int asteroidsCount)
        {
            _spaceship = new Spaceship(_settings.Spaceship, Vector2.zero, _settings.ElementRadius);
            ElementCreate?.Invoke(_spaceship);
            
            _asteroids.Build(asteroidsCount);
        }

        public void ApplyShot()
        {
            _bullets.Create(
                _spaceship.Position + _spaceship.Direction, 
                _spaceship.Direction * _settings.Spaceship.Bullet.Speed);
        }

        public void ApplyRotation(float degrees)
        {
            _spaceship.ApplyRotation(degrees);
        }

        public void ApplyThrust(float force)
        {
            _spaceship.ApplyThrust(force);
        }

        public void Update(float deltaTime)
        {
            _spaceship.ApplyMovement(deltaTime, _field);
            
            _asteroids.Update(deltaTime);
            _bullets.Update(deltaTime);

            if (_asteroids.HasAnyIntersection(_spaceship))
            {
                GameOver?.Invoke();
            }
        }

        private void OnElementCreate(IElement element) => ElementCreate?.Invoke(element);
        
        private void OnElementRemove(IElement element) => ElementRemove?.Invoke(element);
    }
}