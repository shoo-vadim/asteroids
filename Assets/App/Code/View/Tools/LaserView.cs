using UnityEngine;

namespace App.Code.View.Tools
{
    public class LaserView : MonoBehaviour
    {
        [Range(0, 255)]
        [SerializeField] private float _distance;
        [SerializeField] private LineRenderer _line;

        private void Awake()
        {
            _line.gameObject.SetActive(false);
        }

        public void Setup(Ray2D ray)
        {
            _line.SetPosition(0, ray.origin);
            _line.SetPosition(1, ray.origin + ray.direction * _distance);
            _line.gameObject.SetActive(true);
        }
        
        private void OnValidate()
        {
            _line.SetPosition(0, Vector2.zero);
            _line.SetPosition(1, Vector2.one * _distance);
        }
    }
}