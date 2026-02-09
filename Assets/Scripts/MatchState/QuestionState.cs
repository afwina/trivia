using Game.Input;
using UnityEngine;

namespace Game.MatchState
{
    public class QuestionState : MatchState
    {
        public static string Name => "QuestionState";
        
        private QuestionBank m_QuestionBank = new QuestionBank();
        private float m_Timer;
        private float m_Duration = 30;
        
        public QuestionState(MatchStateContext context) : base(context)
        {
            
        }
        
        public override void OnEnter()
        { 
            Context.GameStatus.ResetPlayerSelections();
            
            Question question = m_QuestionBank.GetQuestion();
            Context.GameStatus.SetQuestion(question, m_Duration);
            Context.MatchController.StartQuestion();
            Context.MatchController.OnPlayerAnswer += OnInputAction;
        }
        
        public override void Update()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer > m_Duration)
            {
                Context.StateMachine.ChangeState(AnswerState.Name);
            }
        }

        public override void OnExit()
        {
            m_Timer = 0f;
            Context.MatchController.EndQuestion();
        }

        private void OnInputAction(PlayerChoiceAction action)
        {
            if (!Context.GameStatus.HasPlayerSelectedChoice(action.PlayerId))
            {
                Context.GameStatus.SetPlayerChoice(action.PlayerId, action.Choice);
            }

            if (Context.GameStatus.HaveAllPlayersSelectedChoice())
            {
                Context.StateMachine.ChangeState(AnswerState.Name);
            }
        }
    }
}