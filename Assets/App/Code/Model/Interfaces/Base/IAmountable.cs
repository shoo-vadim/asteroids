using System;

namespace App.Code.Model.Interfaces.Base
{
    public interface IAmountable
    {
        public event Action<int> AmountChange;
        
        public int Amount { get; }
    }
}