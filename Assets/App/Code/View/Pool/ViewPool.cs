﻿using System;
using System.Linq;
using App.Code.Settings.Scriptables;
using UnityEngine;

namespace App.Code.View.Pool
{
    public class ViewPool : MonoBehaviour
    {
        [SerializeField] private ViewPreset _preset;

        private ViewRoot _root;

        private void Awake()
        {
            if (!(_root = GetComponentInChildren<ViewRoot>()))
            {
                throw new InvalidOperationException("Unable to find ViewRoot markup down in hierarchy");
            }
        }

        public MonoView Obtain(ElementType elementType, Vector2 position)
        {
            var prefab = _preset.Views.Single(v => v.ElementType == elementType);

            if (!prefab)
            {
                throw new ArgumentException($"Unable to find prefab for element {elementType}");
            }
            
            var view = Instantiate(prefab, _root.transform);
            view.transform.position = position;
            
            return view;
        }

        public void Release(MonoView view)
        {
            Destroy(view.gameObject);
        }
    }
}