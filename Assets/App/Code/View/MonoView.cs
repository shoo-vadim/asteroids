using App.Code.Model.Binding;
using UnityEngine;

namespace App.Code.View
{
    public class MonoView : MonoBehaviour
    {
        [field: SerializeField] public ElementType ElementType { get; set; }
    }
}