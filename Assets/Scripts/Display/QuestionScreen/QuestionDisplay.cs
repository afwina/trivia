using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class QuestionDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_QuestionText;
        [SerializeField] private OptionWidget[] m_OptionWidgets;
        
        public void Setup(Question question)
        {
            gameObject.SetActive(true);
            m_QuestionText.text = question.QuestionText;
            for (int i = 0; i < question.Options.Length && i < m_OptionWidgets.Length; i++)
            {
                m_OptionWidgets[i].Setup(question.Options[i]);
                m_OptionWidgets[i].Highlight(false);
            }
        }

        public void HighlightCorrectAnswer()
        {
            for (int i = 0; i < m_OptionWidgets.Length; i++)
            {
                if (m_OptionWidgets[i].IsCorrect)
                {
                    m_OptionWidgets[i].Highlight(true);
                }
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }

}
