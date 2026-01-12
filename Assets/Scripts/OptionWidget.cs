using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class OptionWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_OptionText;
        public void Setup(OptionData optionData)
        {
            m_OptionText.text = optionData.OptionText;
        }
    }
}