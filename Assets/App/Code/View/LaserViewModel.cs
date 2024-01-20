using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserViewModel : MonoBehaviour
    {
        // function getIntersectionPoint(ray, circle) {
        //     const [A, B] = ray;
        //     const C = { x: circle.x, y: circle.y };
        //     const r = circle.radius;
        //
        //     const a = (B.x - A.x) ** 2 + (B.y - A.y) ** 2;
        //     const b = 2 * ((B.x - A.x) * (A.x - C.x) + (B.y - A.y) * (A.y - C.y));
        //     const c = (A.x - C.x) ** 2 + (A.y - C.y) ** 2 - r ** 2;
        //     const discriminant = b ** 2 - 4 * a * c;
        //
        //     const result = [];
        //
        //     if(discriminant === 0) {
        //         const t = -b / (2 * a);
        //
        //         if(t >= 0) {
        //             const x = t * (B.x - A.x) + A.x;
        //             const y = t * (B.y - A.y) + A.y;
        //
        //             result.push({ x, y });
        //         }
        //     } else if(discriminant > 0) {
        //         const discriminantSqrt = Math.sqrt(discriminant);
        //         const t1 = (-b + discriminantSqrt) / (2 * a);
        //         const t2 = (-b - discriminantSqrt) / (2 * a);
        //
        //         if(t1 >= 0) {
        //             const x = t1 * (B.x - A.x) + A.x;
        //             const y = t1 * (B.y - A.y) + A.y;
        //
        //             result.push({ x, y })
        //         }
        //
        //         if(t2 >= 0) {
        //             const x = t2 * (B.x - A.x) + A.x;
        //             const y = t2 * (B.y - A.y) + A.y;
        //
        //             result.push({ x, y });
        //         }
        //     }
        //
        //     return result;
        // }
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
            var m = _camera.ScreenToWorldPoint(point);
            _line.SetPosition(1, new Vector3(m.x, m.y));
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
    }
}