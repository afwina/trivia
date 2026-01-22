using UnityEngine;

namespace Game.MatchState
{
    public class AnswerState: MatchState
    {
        public static string Name => "AnswerState";
        private float m_Timer;
        private float m_Duration = 3;
        
        public AnswerState(MatchStateContext context) : base(context)
        {
            
        }
        
        public override void OnEnter()
        {
            Context.GameStatus.RevealAnswer();
        }
        
        public override void Update()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer > m_Duration)
            {
                Context.StateMachine.ChangeState(ScoringState.Name);
            }
        }

        public override void OnExit()
        {
            m_Timer = 0f;
        }
    }
}