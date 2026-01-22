using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerWidget[] m_PlayerWidgets;
        private Dictionary<string, PlayerWidget> m_PlayerWidgetById = new Dictionary<string, PlayerWidget>();
        public void Init(List<PlayerScoreData> playerData)
        {
            for (int i = 0; i < playerData.Count && i < m_PlayerWidgets.Length; i++)
            {
                m_PlayerWidgets[i].Init(playerData[i].PlayerId, playerData[i].Score);
                m_PlayerWidgetById.Add(playerData[i].PlayerId, m_PlayerWidgets[i]);
            }
        }

        public void AddScore(List<ScoreAddedData> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                m_PlayerWidgetById[data[i].PlayerId].PopupScore(data[i].Amount, data[i].TotalScore);
            }
        }

        public void OnPlayerChoiceSelected(string playerId, int choice)
        {
            m_PlayerWidgetById[playerId].SetHighlight(EHighlightState.Deemphasize);
        }

        public void ResetHighlights()
        {
            for (int i = 0; i < m_PlayerWidgets.Length; i++)
            {
                m_PlayerWidgets[i].SetHighlight(EHighlightState.Regular);
            }
        }
    }
}