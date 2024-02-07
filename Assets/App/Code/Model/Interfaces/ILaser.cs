using System;

namespace App.Code.Model.Interfaces
{
    public interface ILaser
    {
        public event Action<float> ReloadChange;
        public event Action<int> AmountChange;
    }
}