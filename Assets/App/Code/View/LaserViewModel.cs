using System;
using App.Code.Model.Proto;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code.View
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserViewModel : MonoBehaviour
    {
        [SerializeField] private Transform _intersection;
        [SerializeField] private Transform _target;
        
        private LineRenderer _line;
        private Camera _camera;

        private void Awake()
        {
            SetupCamera();
        }

        private void Update()
        {
            if (Mouse.current.leftButton.isPressed)
            {
                // DrawLineRenderer(
                //     transform.position, 
                //     _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()),
                //     _target.position);
            }
        }

        private void OnValidate()
        {
            SetupLine();
        }

        // private void DrawLineRenderer(Vector2 origin, Vector2 destination, Vector2 point)
        // {
        //     var r = 1.0f;
        //     // _line.SetPosition(0, origin);
        //     // _line.SetPosition(1, destination);
        //
        //     var direction = (destination - origin).normalized;
        //     var dot = Vector2.Dot(point - origin, direction);
        //
        //     // _intersection.position = closest;
        //     
        //     if ((origin + direction * dot - point).sqrMagnitude < r * r && dot > 0)
        //     {
        //         Debug.Log("Intersected!");
        //     }
        // }

        private void SetupCamera()
        {
            if (!Camera.main)
            {
                throw new NullReferenceException("Unable to find main camera");
            }
            
            _camera = Camera.main;
        }

        private void SetupLine()
        {
            _line = GetComponent<LineRenderer>();
            _line.useWorldSpace = true;
            _line.positionCount = 2;
        }
    }
}