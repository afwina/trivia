using System.Collections.Generic;
using Game.MatchState;
using UnityEngine;

namespace Game
{
    public class QuestionScreen: MonoBehaviour, IScreen
    {
        [SerializeField] private QuestionDisplay m_QuestionDisplay;
        [SerializeField] private PlayerDisplay m_PlayerDisplay;

        public void Init(GameStatus gameStatus)
        {
            gameStatus.OnQuestionSet = StartQuestion;
            gameStatus.OnPlayerChoiceSet = m_PlayerDisplay.OnPlayerChoiceSelected;
            gameStatus.OnAnswerReveal = ShowAnswer;
            gameStatus.OnScoreAdded = ShowScoring;
            
            m_PlayerDisplay.Init(gameStatus.GetPlayerScores());
            gameObject.SetActive(true);
        }
        
        private void StartQuestion(QuestionContext questionContext)
        {
            m_QuestionDisplay.Setup(questionContext.Question);
        }

        private void ShowAnswer(int correctAnswer)
        {
            m_QuestionDisplay.HighlightCorrectAnswer();
            m_PlayerDisplay.ResetHighlights();
        }

        private void ShowScoring(List<ScoreAddedData> data)
        {
            m_PlayerDisplay.AddScore(data);
        }

        public void Close()
        {
            gameObject.SetActive(false);

        }
    }
}