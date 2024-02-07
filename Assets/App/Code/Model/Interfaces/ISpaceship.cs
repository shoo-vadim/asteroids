using App.Code.Model.Interfaces.Base;

namespace App.Code.Model.Interfaces
{
    public interface ISpaceship : IPositionable, IDirectionable 
    {
        public void ApplyRotation(float degrees);
        public void ApplyThrust(float force);
    }
}