using System.Collections.Generic;
using System.Text.RegularExpressions;
using Game.Input;
using Game.Match;
using UnityEngine;

namespace Game.MatchState
{
    public class MatchStateMachine
    {
        private MatchState m_State;
        private Dictionary<string, MatchState> m_States;
        private MatchStateContext m_Context;
        
        public MatchStateMachine(GameStatus gameStatus, IMatchController matchController)
        {
            m_Context = new MatchStateContext
            {
                StateMachine = this , 
                GameStatus = gameStatus,
                MatchController = matchController
            };
            
            m_States = new Dictionary<string, MatchState>
            {
                {QuestionState.Name, new QuestionState(m_Context)},
                {AnswerState.Name, new AnswerState(m_Context)},
                {ScoringState.Name, new ScoringState(m_Context)}
            };
            
            SetState(m_States[QuestionState.Name]);
        }
        
        public void ChangeState(string name)
        {
           if (m_States.TryGetValue(name, out MatchState state))
           {
               m_State.OnExit();
               SetState(state);
               Debug.Log($"MatchState changed to '{name}'");
           }
           else
           {
               Debug.LogWarning($"MatchState '{name}' does not exist. State could not be changed.");
           }
        }

        private void SetState(MatchState state)
        {
            m_State = state;
            m_State.OnEnter();
        }

        public void Update()
        {
            m_State.Update();
        }
    }

    public class MatchStateContext
    {
        public MatchStateMachine StateMachine;
        public GameStatus GameStatus;
        public IMatchController MatchController;
    }

    public class QuestionContext
    {
        public Question Question;
        public float DurationSeconds;

        public int GetCorrectChoice()
        {
            for (int i = 0; i < Question.Options.Length; i++)
            {
                if (Question.Options[i].IsCorrect)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public class PlayersState
    {
        public Dictionary<string, PlayerState> PlayerStateById;
    }

    public class PlayerState
    {
        public int Selection;
        public bool HasSelected;
        public int Score;

        public void ResetSelection()
        {
            Selection = 0;
            HasSelected = false;
        }
        
    }
}