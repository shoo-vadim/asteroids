using UnityEngine;

namespace App.Code.View
{
    public class SelfDestruction : MonoBehaviour
    {
        [Range(0, 2)]
        [SerializeField] private float _time;

        private float _current;

        private void Update()
        {
            _current += Time.deltaTime;

            if (_current > _time)
            {
                Destroy(gameObject);
            }
        }
    }
}