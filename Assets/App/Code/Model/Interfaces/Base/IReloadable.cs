using System;

namespace App.Code.Model.Interfaces.Base
{
    public interface IReloadable
    {
        public event Action<float> ReloadChange;
        
        public float Reload { get; }
    }
}