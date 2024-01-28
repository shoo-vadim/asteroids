using UnityEngine;

namespace App.Code.View
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(LineView))]
    public class PolygonView : MonoBehaviour
    {
        [Range(0.1f, 2f)]
        [SerializeField] private float _radius;
        [Range(3f, 42f)]
        [SerializeField] private int _segments;

        private LineRenderer _line;

        private void CreatePoints()
        {
            // Should be 1 segment more to fix loop gap
            var points = _line.positionCount = _segments + 1 + 1;

            var angle = 0f;
            var step = 360f / _segments;

            for (var i = 0; i < points; i++)
            {
                var x = Mathf.Sin(Mathf.Deg2Rad * angle) * _radius;
                var y = Mathf.Cos(Mathf.Deg2Rad * angle) * _radius;

                _line.SetPosition(i, new Vector3(x, y, 0));

                angle += step;
            }
        }

        private void OnValidate()
        {
            _line = GetComponent<LineRenderer>();
            _line.useWorldSpace = false;

            CreatePoints();
        }
    }
}