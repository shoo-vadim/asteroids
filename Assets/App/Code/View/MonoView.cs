using UnityEngine;

namespace App.Code.View
{
    public abstract class MonoView : MonoBehaviour
    {
        public void Refresh(Vector2 position)
        {
            transform.position = position;
        }
    }
}