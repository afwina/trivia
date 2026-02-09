using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Input;
using Game.Network;
using Newtonsoft.Json;

namespace Game.Match
{
    public class NetworkMatchController :IMatchController
    {
        public Action<PlayerChoiceAction> OnPlayerAnswer { get; set; }

        private WebSocketHandler m_Server = new WebSocketHandler();
        private HTTPHandler m_HttpHandler = new HTTPHandler();

        private MatchData m_MatchData;
        
        public async Task<MatchData> RequestMatch()
        { 
            RequestMatchResponse resp = await m_HttpHandler.POSTRequestMatch();
            await m_Server.Connect();
            m_Server.SendMessage(EventNames.GAME_CLIENT_JOIN, resp.JoinCode);
            m_Server.AddMessageListener(EventNames.PLAYER_JOINED, OnPlayerJoined);
            m_MatchData =  new MatchData{ JoinCode = resp.JoinCode, Players = new List<PlayerData>()};

            return m_MatchData;
        }

        public MatchData LaunchMatch()
        {
            m_Server.RemoveMessageListener(EventNames.PLAYER_JOINED);
            m_Server.AddMessageListener(EventNames.START_MATCH, OnMatchStarted, OnMatchStartFailed);
            m_Server.SendMessage(EventNames.START_MATCH, m_MatchData.JoinCode);
            
            return m_MatchData;
        }

        public void EndMatch()
        {
            m_Server.SendMessage(EventNames.END_MATCH, m_MatchData.JoinCode);
        }

        public void StartQuestion()
        {
            m_Server.SendMessage(EventNames.START_QUESTION, m_MatchData.JoinCode);
            m_Server.AddMessageListener(EventNames.PLAYER_ANSWER, HandlePlayerAnswer);
        }

        public void EndQuestion()
        {
            m_Server.SendMessage(EventNames.CLOSE_QUESTION, m_MatchData.JoinCode);
            m_Server.RemoveMessageListener(EventNames.PLAYER_ANSWER);
        }
        
        private void OnPlayerJoined(string data)
        {
            PlayerJoinedData playerJoinedData = JsonConvert.DeserializeObject<PlayerJoinedData>(data);
            m_MatchData.Players.Add(new PlayerData{Name = playerJoinedData.Name});
        }

        private void OnMatchStarted(string data)
        {
            m_Server.RemoveMessageListener(EventNames.START_MATCH);
        }

        private void OnMatchStartFailed(string data)
        {
            m_Server.RemoveMessageListener(EventNames.START_MATCH);
        }

        private void HandlePlayerAnswer(string data)
        {
            PlayerAnswerData answerData = JsonConvert.DeserializeObject<PlayerAnswerData>(data);
            PlayerChoiceAction action = new PlayerChoiceAction(answerData.PlayerId, answerData.Answer);
            OnPlayerAnswer?.Invoke(action);
        }
    }
}