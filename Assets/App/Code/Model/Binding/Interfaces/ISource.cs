using System;

namespace App.Code.Model.Binding.Interfaces
{
    public interface ISource
    {
        public event Action<IPositionable> ElementCreate;
        public event Action<IPositionable> ElementRemove;
    }
}