using System.Collections.Generic;
using UnityEngine;

namespace Match.MatchState
{
    public class ScoringState: MatchState
    {
        public static string Name => "ScoringState";
        private const int m_CorrectAnswerScore = 100;
        
        public ScoringState(MatchStateContext context) : base(context)
        {
            
        }
        
        public override void OnEnter()
        {
            List<string> correct = Context.GameStatus.GetCorrectPlayers();
            List<AddScoreData> toAdd = new List<AddScoreData>(correct.Count);
            for (int i = 0; i < correct.Count; i++)
            {
                toAdd.Add(new AddScoreData{PlayerId = correct[i], Amount = m_CorrectAnswerScore});
            }
            
            Context.GameStatus.AddScore(toAdd);
            Context.StateMachine.ChangeState(QuestionState.Name);
        }

        public override void Update()
        {
            
        }
        
        public override void OnExit()
        {
            
        }
    }
}