using System;
using UnityEngine;

namespace Game
{
    public class StartScreen : MonoBehaviour, IScreen
    {
        private Action m_OnStartClicked;

        public void Init(Action onStart)
        {
            m_OnStartClicked = onStart;
            gameObject.SetActive(true);
        }
        
        public void Button_OnStartClicked()
        {
            m_OnStartClicked?.Invoke();
        }

        public void Close()
        {
            m_OnStartClicked = null;
            gameObject.SetActive(false);
        }
    }
}