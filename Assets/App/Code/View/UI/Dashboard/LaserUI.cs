using App.Code.Model.Interfaces;
using App.Code.View.Interfaces;
using UnityEngine;

namespace App.Code.View.UI.Dashboard
{
    public class LaserUI : MonoBehaviour, IBindable<ILaser>
    {
        [SerializeField] private IndicatorView _indicatorAmount;
        [SerializeField] private IndicatorView _indicatorReload;
        
        public void Bind(ILaser bind)
        {
            bind.AmountChange += OnAmountChange;
            bind.ReloadChange += OnReloadChange;
        }

        public void Drop(ILaser bind)
        {
            bind.AmountChange -= OnAmountChange;
            bind.ReloadChange -= OnReloadChange;
        }
        
        private void OnAmountChange(int amount) => 
            _indicatorAmount.Refresh(amount.ToString());

        private void OnReloadChange(float reload) => 
            _indicatorReload.Refresh(reload.ToString("00.00"));
    }
}