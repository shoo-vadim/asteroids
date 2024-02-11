using System;
using App.Code.View.UI.Dashboard;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code.View.UI
{
    public class GameUI : MonoBehaviour
    {
        public event Action Restart;
        
        public SpaceshipUI Spaceship { get; private set; }
        public LaserUI Laser { get; private set; }
        
        private PointsUI _points;

        private bool _flagPoints;
        
        private void Awake()
        {
            Spaceship = 
                GetComponentInChildren<SpaceshipUI>() 
                ?? throw new InvalidOperationException($"Unable to find {typeof(SpaceshipUI).FullName} component!");
            
            Laser = 
                GetComponentInChildren<LaserUI>() 
                ?? throw new InvalidOperationException($"Unable to find {typeof(LaserUI).FullName} component!");
            
            _points =
                GetComponentInChildren<PointsUI>()
                ?? throw new InvalidOperationException($"Unable to find {typeof(PointsUI).FullName} component!");

            Laser.gameObject.SetActive(true);
            Spaceship.gameObject.SetActive(true);
            _points.gameObject.SetActive(false);
        }

        public void ShowPoints(int count)
        {
            Spaceship.gameObject.SetActive(false);
            Laser.gameObject.SetActive(false);
            _flagPoints = true;
            _points.gameObject.SetActive(true);
            _points.Refresh(count);
        }

        private void Update()
        {
            if (_flagPoints && Keyboard.current.fKey.wasPressedThisFrame)
            {
                Restart?.Invoke();
            }
        }
    }
}