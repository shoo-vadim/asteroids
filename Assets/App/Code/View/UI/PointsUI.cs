using TMPro;
using UnityEngine;

namespace App.Code.View.UI
{
    public class PointsUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _points;

        public void Refresh(int count)
        {
            _points.text = count.ToString();
        }
    }
}