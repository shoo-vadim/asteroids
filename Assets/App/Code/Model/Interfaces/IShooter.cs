using UnityEngine;

namespace App.Code.Model.Interfaces
{
    public interface IShooter
    {
        public bool TryApplyBulletShot();
        public bool TryApplyLaserShot(out Ray2D ray);
    }
}