using System;

namespace App.Code.Model.Interfaces.Base
{
    public interface ISource<out TElement> where TElement : IPositionable
    {
        public event Action<TElement> Create;
        public event Action<TElement> Remove;
    }
}