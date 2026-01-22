using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class OptionWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_OptionText;
        [SerializeField] private GameObject m_Highlight;
        public bool IsCorrect;
        
        public void Setup(OptionData optionData)
        {
            m_OptionText.text = optionData.OptionText;
            IsCorrect = optionData.IsCorrect;
        }

        public void Highlight(bool value)
        {
            m_Highlight.SetActive(value);
        }
    }
}