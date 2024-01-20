using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code
{
    public class Laser : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            if (!Camera.main)
            {
                throw new NullReferenceException("Unable to find main camera");
            }
            
            _camera = Camera.main;
        }

        private void Update()
        {
            Debug.Log(_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }
    }
}