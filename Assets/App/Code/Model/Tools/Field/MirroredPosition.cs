using UnityEngine;

namespace App.Code.Model.Logical.Field
{
    public readonly struct MirroredPosition
    {
        public readonly bool WasMirrored;
        public readonly Vector2 Position;

        public MirroredPosition(bool wasMirrored, Vector2 position)
        {
            WasMirrored = wasMirrored;
            Position = position;
        }

        public void Deconstruct(out bool has, out Vector2 position)
        {
            has = WasMirrored;
            position = Position;
        }

        public override string ToString()
        {
            return $"{WasMirrored} {Position}";
        }
    }
}