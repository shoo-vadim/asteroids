using TMPro;
using UnityEngine;

namespace App.Code.View.UI.Dashboard
{
    public class IndicatorView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _body;

        public void Refresh(object text)
        {
            _body.text = text.ToString();
        }
    }
}