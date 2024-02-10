using System.Collections.Generic;
using App.Code.Model.Entities;
using App.Code.Model.Logical.Extensions;
using App.Code.View.Binding;
using App.Code.View.Binding.Custom;
using App.Code.View.Pool;

namespace App.Code.View.Elements
{
    public class AsteroidElements : ViewElements
    {
        private readonly Dictionary<Asteroid, PositionableView<Asteroid>> _asteroids = new();
        
        public void CreateAsteroid(Asteroid asteroid)
        {
            _asteroids.Add(asteroid, ObtainElement<AsteroidView, Asteroid>(
                asteroid.IsFragment ? ElementType.Fragment : ElementType.Asteroid, asteroid));
        }

        public void RemoveAsteroid(Asteroid asteroid)
        {
            ReleaseElement(_asteroids.GetAndRemove(asteroid), asteroid);
        }
    }
}