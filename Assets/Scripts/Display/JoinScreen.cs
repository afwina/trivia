using System;
using UnityEngine;

namespace Game
{
    public class JoinScreen : MonoBehaviour, IScreen
    {
        private Action m_OnLaunchMatch;

        public void Init(string joinCode, Action onLaunchMatch)
        {
            m_OnLaunchMatch = onLaunchMatch;
            gameObject.SetActive(true);
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
            m_OnLaunchMatch = null;
        }
    }
}