using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code.Settings.Scriptables
{
    [CreateAssetMenu(fileName = "Game Input Preset", menuName = "Game Input Preset", order = 0)]
    public class GameInputPreset : ScriptableObject
    {
        [field: SerializeField] public InputAction Rotate { get; private set; }
        [field: SerializeField] public InputAction Thrust { get; private set; }
        [field: SerializeField] public InputAction Bullet { get; private set; }
        [field: SerializeField] public InputAction Laser { get; private set; }

        public void Enable()
        {
            Rotate.Enable();
            Thrust.Enable();
            Bullet.Enable();
            Laser.Enable();
        }
        
        public void Disable()
        {
            Rotate.Disable();
            Thrust.Disable();
            Bullet.Disable();
            Laser.Disable();
        }
    }
}