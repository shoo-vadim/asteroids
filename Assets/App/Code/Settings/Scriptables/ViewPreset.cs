using App.Code.View;
using UnityEngine;

namespace App.Code.Settings.Scriptables
{
    [CreateAssetMenu(fileName = "View Preset", menuName = "View Preset", order = 0)]
    public class ViewPreset : ScriptableObject
    {
        [SerializeField] private MonoView[] _views;

        public MonoView[] Views => _views;
    }
}