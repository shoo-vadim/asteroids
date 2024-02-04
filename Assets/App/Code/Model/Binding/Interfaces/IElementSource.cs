using System;

namespace App.Code.Model.Binding.Interfaces
{
    public interface IElementSource
    {
        public event Action<IElement> ElementCreate;
        
        public event Action<IElement> ElementRemove;
    }
}