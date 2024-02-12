using TMPro;
using UnityEngine;

namespace App.Code.View.UI
{
    public class HintUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        public void Refresh(string bullet, string laser)
        {
            _text.text = $"{bullet} - bullet \n {laser} - laser";
        }
    }
}