using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public class JoinScreen : MonoBehaviour, IScreen
    {
        [SerializeField] private TMP_Text m_MatchCode;
        private Action m_OnLaunchMatch;
        private Action m_OnBack;

        public void Init(string joinCode, Action onLaunchMatch, Action onBack)
        {
            m_MatchCode.text = joinCode;
            m_OnLaunchMatch = onLaunchMatch;
            m_OnBack = onBack;
            gameObject.SetActive(true);
        }

        public void Button_OnLaunchMatchClicked()
        {
            m_OnLaunchMatch?.Invoke();
        }
        
        public void Button_OnBackClicked()
        {
            m_OnBack?.Invoke();
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
            m_OnLaunchMatch = null;
        }
    }
}