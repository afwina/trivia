using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class QuestionDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_QuestionText;
        [SerializeField] private OptionWidget[] m_OptionWidgets;
        
        public void Setup(Question question)
        {
            m_QuestionText.text = question.QuestionText;
            for (int i = 0; i < question.Options.Length && i < m_OptionWidgets.Length; i++)
            {
                m_OptionWidgets[i].Setup(question.Options[i]);
            }
        }
    }

}
