using System;
using App.Code.Model.Entities;
using App.Code.Model.Interfaces.Base;
using App.Code.Model.Logical.Field;
using UnityEngine;

namespace App.Code.Model.Logic
{
    public class SpaceshipModel : ISource<Spaceship>
    {
        public event Action<Spaceship> Create;
        public event Action<Spaceship> Remove;
        
        public event Action Dead;

        private readonly GameField _field;
        private Spaceship _spaceship;

        public SpaceshipModel(GameField field, Spaceship spaceship)
        {
            _field = field;
            _spaceship = spaceship;
        }

        public void ApplyRotation(float degrees)
        {
            _spaceship?.ApplyRotation(degrees);
        }

        public void ApplyThrust(float force)
        {
            _spaceship?.ApplyThrust(force);
        }

        public bool ApplyBullet(Vector2 position)
        {
            if (_spaceship == null || !_spaceship.HasIntersectionWithPoint(position))
            {
                return false;
            }
            
            DestroySpaceship();
            return true;
        }

        public bool IsAlive()
        {
            return _spaceship != null;
        }

        public bool ApplyBody(Vector2 position, float radius)
        {
            if (_spaceship == null || !_spaceship.HasIntersectionWithBody(position, radius))
            {
                return false;
            }
            
            DestroySpaceship();
            return true;
        }

        public bool TryGetPosition(out Vector2 position)
        {
            if (_spaceship == null)
            {
                position = default;
                return false;
            }

            position = _spaceship.Position;
            return true;
        }

        public bool TryGetShoot(out Vector2 point, out Vector2 direction)
        {
            if (_spaceship == null)
            {
                point = direction = Vector2.zero;
                return false;
            }

            point = _spaceship.ShootingPoint;
            direction = _spaceship.Direction;
            return true;
        }

        private void DestroySpaceship()
        {
            var spaceship = _spaceship;
            _spaceship = null;
            Dead?.Invoke();
            Remove?.Invoke(spaceship);
        }

        public void Update(float deltaTime)
        {
            _spaceship?.ApplyMovement(deltaTime, _field);
        }
    }
}