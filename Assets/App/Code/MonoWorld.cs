using App.Code.Model.Proto;
using UnityEngine;

namespace App.Code
{
    public class MonoWorld : MonoBehaviour
    {
        private OpenSpace _space;
        
        private void Awake()
        {
            _space = new OpenSpace(new Field(10, 5), 30);
        }

        private void Update()
        {
            _space.Update(Time.deltaTime);
        }
    }
}