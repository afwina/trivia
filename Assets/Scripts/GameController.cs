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

        private void Awake()
        {
            m_DisplayController.ShowStartScreen(StartMatch);
            m_MatchStateMachine = new MatchStateMachine(new GameStatus(new MatchData()));
        }

        private async void StartMatch()
        {
           MatchData md = await m_MatchController.RequestMatch();
           m_DisplayController.ShowJoinScreen(md.JoinCode, StartGame);
        }

        private void StartGame()
        {
            MatchData md = m_MatchController.LaunchMatch();
            GameStatus gameStatus = new GameStatus(md);

            m_DisplayController.ShowGameScreen(gameStatus);
            m_InputController.OnInputAction += OnInputAction;
        }
        
        private void Update()
        {
            m_MatchStateMachine.Update();
        }

        private void OnInputAction(AInputAction action)
        {
            m_MatchStateMachine.OnInputAction(action);
        }
    }
}
