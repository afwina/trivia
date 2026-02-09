using System;
using System.Threading.Tasks;
using Game.Input;

namespace Game.Match
{
    public interface IMatchController
    {
        public Task<MatchData> RequestMatch();
        public MatchData LaunchMatch();
        public void EndMatch();

        public void StartQuestion();
        public void EndQuestion();
        public Action<PlayerChoiceAction> OnPlayerAnswer { get; set; }
    }
}