using System;
using App.Code.Model.Interfaces;
using App.Code.Settings;

namespace App.Code.Model.Logic
{
    public class LaserModel : ILaser
    {
        public event Action<float> ReloadChange;
        public event Action<int> AmountChange;
        
        public float Reload { get; private set; }

        public int Amount { get; private set; }

        public bool CanApplyShot => Reload <= 0 && Amount > 0;

        private readonly float _reloadFull;

        public LaserModel(LaserSettings settings)
        {
            _reloadFull = settings.Reload;
            Amount = settings.Amount;
        }

        public void ApplyShot()
        {
            Reload = _reloadFull;
            ReloadChange?.Invoke(Reload);

            Amount--;
            AmountChange?.Invoke(Amount);
        }

        public void Update(float delta)
        {
            if (Reload > 0)
            {
                Reload -= delta;
                ReloadChange?.Invoke(Reload);
            }
        }
    }
}