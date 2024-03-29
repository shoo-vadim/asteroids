﻿using UnityEngine;

namespace App.Code.View
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineView : MonoBehaviour
    {
        [Range(0.01f, 1.0f)]
        [SerializeField] private float _width;

        private void OnValidate()
        {
            var line = GetComponent<LineRenderer>();
            line.startWidth = line.endWidth = _width;
        }
    }
}