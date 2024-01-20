using System;
using App.Code.Model;
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
                DrawLineRenderer(Mouse.current.position.ReadValue());
            }
        }

        private void OnValidate()
        {
            SetupLine();
        }

        private void DrawLineRenderer(Vector2 point)
        {
            var p = transform.position;
            var m = _camera.ScreenToWorldPoint(point);
            var destination = new Vector3(m.x, m.y);
            _line.SetPosition(0, p);
            _line.SetPosition(1, destination);
            var has = HasIntersection(
                new Laser(p, destination), 
                new Circle(_target.position, 1f));

            if (has)
            {
                Debug.Log("INTERSECTED!");
            }
        }

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

        // https://github.com/sszczep/ray-casting-in-2d-game-engines
        private static bool HasIntersection(Laser laser, Circle circle)
        {
            var (a, b) = (laser.Source, laser.Destination);
            var c = circle.Position;
            var r = circle.Radius;

            var v1 = Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2);
            var v2 = 2 * ((b.x - a.x) * (a.x - c.x) + (b.y - a.y) * (a.y - c.y));
            var v3 = Mathf.Pow(a.x - c.x, 2) + Mathf.Pow(a.y - c.y, 2) - Mathf.Pow(r, 2);
            var discriminant = Mathf.Pow(v2, 2) - 4 * v1 * v3;

            return discriminant >= 0;
        }
    }
}