using System.Threading.Tasks;
using Game.Network;

namespace Game.Match
{
    public class NetworkMatchController :IMatchController
    {
        private WebSocketHandler m_Server = new WebSocketHandler();
        private HTTPHandler m_HttpHandler = new HTTPHandler();
        
        public async Task<MatchData> RequestMatch()
        { 
            RequestMatchResponse resp = await m_HttpHandler.POSTRequestMatch();
            await m_Server.Connect();
            
            m_Server.SendMessage("gameClientJoin", resp.JoinCode);
            return new MatchData{ JoinCode = resp.JoinCode};
        }

        public MatchData LaunchMatch()
        {
            m_Server.SendMessage("startMatch", "");
            return new MatchData();
        }

        public void EndMatch()
        {
            m_Server.SendMessage("endMatch", "");
        }
    }
}