using System;
using System.Linq;
using App.Code.Model.Custom.Asteroids;
using App.Code.Model.Entities;
using App.Code.Model.Logical.Field;
using App.Code.Settings;
using App.Code.View.Elements;
using App.Code.World.Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code.World
{
    public class MenuWorld : MonoWorld<AsteroidElements>
    {
        public event Action GameStart;

        private GameField _field;
        private Asteroid[] _asteroids;

        private void Start()
        {
            _field = CreateGameFieldFromCamera();
            _asteroids = new AsteroidBuilder(_field.GetRandomPosition, new Range<float>(1, 5))
                .BuildCollection(22)
                .ToArray();

            foreach (var asteroid in _asteroids)
            {
                View.CreateAsteroid(asteroid);
            }
        }

        private void Update()
        {
            foreach (var asteroid in _asteroids)
            {
                asteroid.ApplyMovement(Time.deltaTime, _field);
            }
            
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                GameStart?.Invoke();
            }
        }

        private void OnDestroy()
        {
            foreach (var asteroid in _asteroids)
            {
                View.RemoveAsteroid(asteroid);
            }
        }
    }
}