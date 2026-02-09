using System;
using System.Collections.Generic;
using Game.Match;
using Game.MatchState;
using UnityEngine;

namespace Game
{
    public class GameStatus
    {
        public Action<QuestionContext> OnQuestionSet;
        public Action<int> OnAnswerReveal;
        public Action<List<ScoreAddedData>> OnScoreAdded;
        public Action<string, int> OnPlayerChoiceSet;

        private QuestionContext m_QuestionContext;
        private PlayersState m_PlayerState;
        private PlayerData[] m_PlayerOrder;

        public GameStatus()
        {
            m_QuestionContext = new QuestionContext();
        }

        public void SetMatchData(MatchData md)
        {
            m_PlayerOrder = md.Players.ToArray();
            Dictionary<string, PlayerState> playersState = new Dictionary<string, PlayerState>();
            for (int i = 0; i < m_PlayerOrder.Length; i++)
            {
                playersState[m_PlayerOrder[i].Name] = new PlayerState();
            }

            m_PlayerState = new PlayersState
            {
                PlayerStateById = playersState
            };
        }

        public void SetQuestion(Question question, float duration)
        {
            m_QuestionContext = new QuestionContext() { Question = question, DurationSeconds = duration };
            OnQuestionSet?.Invoke(m_QuestionContext);
        }

        public void RevealAnswer()
        {
            OnAnswerReveal?.Invoke(m_QuestionContext.GetCorrectChoice());
        }

        public bool HasPlayerSelectedChoice(string playerId)
        {
            PlayerState playerState = GetPlayerState(playerId);
            return playerState.HasSelected;
        }

        public void SetPlayerChoice(string playerId, int choice)
        {
            PlayerState playerState = GetPlayerState(playerId);
            playerState.HasSelected = true;
            playerState.Selection = choice;
            Debug.Log($"{playerId} chose option {choice}");
            
            OnPlayerChoiceSet?.Invoke(playerId, choice);
        }

        public bool HaveAllPlayersSelectedChoice()
        {
            foreach(var kvp in m_PlayerState.PlayerStateById)
            {
                if (!kvp.Value.HasSelected)
                {
                    return false;
                }
            }

            return true;
        }

 
        public List<string> GetCorrectPlayers()
        {
            List<string> players = new();
            int correctChoice = m_QuestionContext.GetCorrectChoice();
            foreach(var kvp in m_PlayerState.PlayerStateById)
            {
                if (!kvp.Value.HasSelected)
                {
                    continue;
                }

                if (kvp.Value.Selection == correctChoice)
                {
                    players.Add(kvp.Key);
                }
            }

            return players;
        }

        public void AddScore(List<AddScoreData> toAdd)
        {
            List<ScoreAddedData> data = new List<ScoreAddedData>(toAdd.Count);
            for (int i = 0; i < toAdd.Count; i++)
            {
                PlayerState ps = GetPlayerState(toAdd[i].PlayerId);
                ps.Score += toAdd[i].Amount;
                data.Add(new ScoreAddedData{PlayerId = toAdd[i].PlayerId, Amount = toAdd[i].Amount, TotalScore = ps.Score});
            }
            
            OnScoreAdded?.Invoke(data);
        }
        
        public void ResetPlayerSelections()
        {
            foreach(var kvp in m_PlayerState.PlayerStateById)
            {
                kvp.Value.ResetSelection();
            }
        }

        public List<PlayerScoreData> GetPlayerScores()
        {
            List<PlayerScoreData> scoreData = new List<PlayerScoreData>(m_PlayerOrder.Length);
            foreach (PlayerData plyData in m_PlayerOrder)
            {
                PlayerState playerState = GetPlayerState(plyData.Name);
                scoreData.Add(new PlayerScoreData{PlayerId = plyData.Name, Score = playerState.Score});
            }

            return scoreData;
        }

        private PlayerState GetPlayerState(string playerId)
        {
            return m_PlayerState.PlayerStateById[playerId];
        }
    }

    public class AddScoreData
    {
        public string PlayerId;
        public int Amount;
    }

    public class ScoreAddedData
    {
        public string PlayerId;
        public int Amount;
        public int TotalScore;
    }

    public class PlayerScoreData
    {
        public string PlayerId;
        public int Score;
    }
}