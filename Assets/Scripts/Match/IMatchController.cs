using System.Threading.Tasks;

namespace Game.Match
{
    public interface IMatchController
    {
        public Task<MatchData> RequestMatch();
        public MatchData LaunchMatch();

        public void EndMatch();
    }
}