using System.Collections.Generic;
using Match.Input;
using Match.MatchState;
using UnityEngine;

namespace Match
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private DisplayController m_DisplayController;
        [SerializeField] private InputController m_InputController;

        private MatchStateMachine m_MatchStateMachine;

        private void Awake()
        {
            var players = new List<string> { "Player_1", "Player_2", "Player_3", "Player_4" };
            GameStatus gameStatus = new GameStatus(players);

            m_DisplayController.Init(gameStatus);
            m_MatchStateMachine = new MatchStateMachine(gameStatus);
            m_InputController.OnInputAction += OnInputAction;
        }

        private void Update()
        {
            m_MatchStateMachine.Update();
        }

        private void OnInputAction(InputAction action)
        {
            m_MatchStateMachine.OnInputAction(action);
        }
    }
}
