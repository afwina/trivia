using System;
using System.Collections.Generic;
using Game.MatchState;
using UnityEngine;

namespace Game
{
    public class DisplayController : MonoBehaviour
    {
        [SerializeField] private StartScreen m_StartScreen;
        [SerializeField] private JoinScreen m_JoinScreen;
        [SerializeField] private QuestionScreen m_QuestionScreen;

        private IScreen m_CurrentScreen;
        
        public void ShowStartScreen(Action onStartMatch)
        {
            SetScreen(m_StartScreen);
            m_StartScreen.Init(onStartMatch);
        }
        
        public void ShowJoinScreen(string joinCode, Action onLaunchMatch, Action onBack)
        {
            SetScreen(m_JoinScreen);
            m_JoinScreen.Init(joinCode, onLaunchMatch, onBack);
        }
        
        public void ShowGameScreen(GameStatus gameStatus)
        {
            SetScreen(m_QuestionScreen);
            m_QuestionScreen.Init(gameStatus);
        }
        
        private void SetScreen(IScreen screen)
        {
            if (m_CurrentScreen != null)
            {
                m_CurrentScreen.Close();
            }
            
            m_CurrentScreen = screen;
        }
    }
}