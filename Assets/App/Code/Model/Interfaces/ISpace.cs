using System;

namespace App.Code.Model.Interfaces
{
    public interface ISpace : IShooter
    {
        public event Action GameOver;
        
        public void Update(float deltaTime);
    }
}