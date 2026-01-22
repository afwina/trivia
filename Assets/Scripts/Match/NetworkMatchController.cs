using System.Threading.Tasks;

namespace Game.Match
{
    public class NetworkMatchController :IMatchController
    {
        private ServerWebSocket m_Server = new ServerWebSocket();
        public async Task<MatchData> RequestMatch()
        {
           await m_Server.Connect();
           //m_Server.SendMessage("gameClientJoin");
           return new MatchData();
        }

        public MatchData LaunchMatch()
        {
            throw new System.NotImplementedException();
        }

        public void EndMatch()
        {
            throw new System.NotImplementedException();
        }
    }
}