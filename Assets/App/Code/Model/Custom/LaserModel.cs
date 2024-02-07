using System;
using App.Code.Model.Interfaces;
using App.Code.Settings;

namespace App.Code.Model.Custom
{
    public class LaserModel : ILaser
    {
        public event Action<float> ReloadChange;
        public event Action<int> AmountChange;

        public bool CanApplyShot => _currentReload <= 0 && _currentAmount > 0;

        private readonly float _reload;
        private float _currentReload;
        private int _currentAmount;

        public LaserModel(LaserSettings settings)
        {
            _reload = settings.Reload;
            _currentAmount = settings.Amount;
        }

        public void ApplyShot()
        {
            _currentReload = _reload;
            ReloadChange?.Invoke(_currentReload);

            _currentAmount--;
            AmountChange?.Invoke(_currentAmount);
        }

        public void Update(float delta)
        {
            if (_currentReload > 0)
            {
                _currentReload -= delta;
                ReloadChange?.Invoke(_currentReload);
            }
        }
    }
}