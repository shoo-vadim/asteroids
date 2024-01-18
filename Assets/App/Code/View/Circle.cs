using System;
using UnityEngine;

namespace App.Code.View
{
    [RequireComponent(typeof(LineRenderer))]
    [ExecuteInEditMode]
    public class Circle : MonoBehaviour
    {
        [SerializeField] private float _radius;

        private readonly float _width = 0.1f;
        private readonly int _segments = 10;

        private LineRenderer _line;
        
        private void CreatePoints()
        {
            var angle = 0f;
            var step = 360f / _segments;

            for (var i = 0; i < _segments + 1; i++)
            {
                var x = Mathf.Sin (Mathf.Deg2Rad * angle) * _radius;
                var y = Mathf.Cos (Mathf.Deg2Rad * angle) * _radius;

                _line.SetPosition (i, new Vector3(x, y, 0));

                angle += step;
            }
        }

        private void OnValidate()
        {
            _line = GetComponent<LineRenderer>();
            _line.positionCount = _segments + 1;
            _line.useWorldSpace = false;
            _line.startWidth = _line.endWidth = _width;
            
            CreatePoints();
        }
    }
}