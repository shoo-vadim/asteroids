using TMPro;
using UnityEngine;

namespace App.Code.UI
{
    public class IndicatorUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _body;

        public void Refresh(string text)
        {
            _body.text = text;
        }
    }
}