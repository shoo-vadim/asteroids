using System;
using App.Code.Model.Logical.Field;
using App.Code.View.Elements;
using UnityEngine;

namespace App.Code.World.Tools
{
    public class MonoWorld<TView> : MonoBehaviour, IWorld 
        where TView : ViewElements
    {
        protected TView View { get; private set; }

        protected virtual void Awake()
        {
            if (!TryGetComponent<TView>(out var view))
            {
                throw new InvalidOperationException($"Unable to find {typeof(TView).FullName} component!");
            }

            View = view;
        }

        protected GameField CreateGameFieldFromCamera()
        {
            if (GetComponentInChildren<Camera>() is not { orthographicSize: var size} worldCamera)
            {
                throw new InvalidOperationException("Camera is not found");
            }

            return new GameField(size * worldCamera.aspect, size);
        }
    }
}