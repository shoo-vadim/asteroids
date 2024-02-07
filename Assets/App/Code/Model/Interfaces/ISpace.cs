using System;

namespace App.Code.Model.Interfaces
{
    public interface ISpace : IShooter
    {
        public event Action GameOver;

        public ISpaceship Spaceship { get; }
        public ILaser Laser { get; }
        
        // Try to move build to the constructor
        public void Build(int asteroidsCount);
        public void Update(float deltaTime);
    }
}