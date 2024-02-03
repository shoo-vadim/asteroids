using System;

namespace App.Code.Model.Binding.Interfaces
{
    public interface ISource
    {
        public event Action<ElementType, IElement> ElementCreate;
        public event Action<IElement> ElementRemove;
    }
}