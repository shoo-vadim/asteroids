﻿namespace App.Code.View.Interfaces
{
    public interface IBindable<in TBind>
    {
        public void Bind(TBind bind);
        public void Drop(TBind bind);
    }
}