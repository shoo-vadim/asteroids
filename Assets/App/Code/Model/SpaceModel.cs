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
    public class SpaceModel : IElementSource
    {
        public event Action<IElement> ElementCreate;
        public event Action<IElement> ElementRemove;

        public Spaceship Spaceship { get; private set; }

        private readonly GameField _field;
        private readonly GameSettings _settings;

        private readonly AsteroidsModel _asteroids;
        private readonly BulletsModel _bullets;

        public SpaceModel(GameField field, GameSettings settings)
        {
            (_field, _settings) = (field, settings);
            
            _asteroids = new AsteroidsModel(field, settings);
            _asteroids.ElementCreate += OnElementCreate;
            _asteroids.ElementRemove += OnElementRemove;

            _bullets = new BulletsModel(field, _asteroids, _settings.Spaceship.Bullet.Lifetime);
            _bullets.ElementCreate += OnElementCreate;
            _bullets.ElementRemove += OnElementRemove;
        }
        
        public void Build()
        {
            Spaceship = new Spaceship(_settings.Spaceship, Vector2.zero, _settings.ElementRadius);
            ElementCreate?.Invoke(Spaceship);
            
            _asteroids.Build(10);
        }

        public void ApplyShot()
        {
            _bullets.Create(
                Spaceship.Position + Spaceship.Direction, 
                Spaceship.Direction * _settings.Spaceship.Bullet.Speed);
        }

        public void Update(float deltaTime)
        {
            Spaceship.ApplyMovement(deltaTime, _field);
            
            _asteroids.Update(deltaTime);
            _bullets.Update(deltaTime);
        }

        private void OnElementCreate(IElement element) => ElementCreate?.Invoke(element);
        
        private void OnElementRemove(IElement element) => ElementRemove?.Invoke(element);
    }
}