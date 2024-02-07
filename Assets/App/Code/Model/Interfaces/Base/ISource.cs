using System;

namespace App.Code.Model.Interfaces.Base
{
    public interface ISource<out TElement>
    {
        public event Action<TElement> Create;
        public event Action<TElement> Remove;
    }
}