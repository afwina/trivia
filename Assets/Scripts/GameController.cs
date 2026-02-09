using System.Collections.Generic;
using Game.Input;
using Game.Match;
using Game.MatchState;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private DisplayController m_DisplayController;
        private IMatchController m_MatchController = new NetworkMatchController();
        private IInputController m_InputController = new ClientsideInputController();
        private MatchStateMachine m_MatchStateMachine;
        private GameStatus m_GameStatus;
        
        private void Awake()
        {
            InitStartScreen();
            m_GameStatus = new GameStatus();
        }

        private void InitStartScreen()
        {
            m_DisplayController.ShowStartScreen(SetupGameLobby);
        }

        private async void SetupGameLobby()
        {
           MatchData md = await m_MatchController.RequestMatch();
           m_DisplayController.ShowJoinScreen(md.JoinCode, StartGame, InitStartScreen);
        }

        private void StartGame()
        {
            MatchData md = m_MatchController.LaunchMatch();
            
            m_GameStatus.SetMatchData(md);
            m_DisplayController.ShowGameScreen(m_GameStatus);
            m_MatchStateMachine = new MatchStateMachine(m_GameStatus, m_MatchController);
        }
        
        private void Update()
        {
            if (m_MatchStateMachine != null)
            {
                m_MatchStateMachine.Update();
            }
        }
    }
}
