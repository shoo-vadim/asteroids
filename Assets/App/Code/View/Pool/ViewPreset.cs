using UnityEngine;

namespace App.Code.View.Pool
{
    [CreateAssetMenu(fileName = "View Preset", menuName = "View Preset", order = 0)]
    public class ViewPreset : ScriptableObject
    {
        [SerializeField] private MonoView[] _views;

        public MonoView[] Views => _views;
    }
}