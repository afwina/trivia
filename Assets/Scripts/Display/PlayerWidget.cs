using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Match
{
    public class PlayerWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_PlayerText;
        [SerializeField] private TMP_Text m_ScoreText;
        [SerializeField] private Image m_Background;
        [SerializeField] private Color m_Regular;
        [SerializeField] private Color m_Deemphasize;

        public void Init(string playerId, int score)
        {
            m_PlayerText.text = playerId;
            m_ScoreText.text = score.ToString();
        }

        public void PopupScore(int addAmount, int finalScore)
        {
            m_ScoreText.text = finalScore.ToString();
        }

        public void SetHighlight(EHighlightState state)
        {
            if (state == EHighlightState.Regular || state == EHighlightState.Highlight)
            {
                m_Background.color = m_Regular;
                transform.localScale = new Vector3(1f,1f,1f);
            } else if (state == EHighlightState.Deemphasize)
            {
                m_Background.color = m_Deemphasize;
                transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
        }
    }

    public enum EHighlightState
    {
        Regular,
        Highlight,
        Deemphasize
    }
}