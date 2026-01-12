using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private QuestionDisplay m_QuestionDisplay;
        private QuestionBank m_QuestionBank;
        
        private void Start()
        {
            m_QuestionBank = new QuestionBank();
            m_QuestionBank.Init();
            EnterQuestionState();
        }

        public void EnterQuestionState()
        {
            Question question = m_QuestionBank.GetQuestion();
            m_QuestionDisplay.Setup(question);
        }
    }

}
