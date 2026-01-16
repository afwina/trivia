using System;
using UnityEngine;

namespace Match.Input
{
    public class InputController : MonoBehaviour
    {
        public Action<InputAction> OnInputAction;
        public void SetPlayerChoice(string playerId, int choice)
        {
            OnInputAction.Invoke(new PlayerChoiceAction(playerId, choice));
        }
    }

    public abstract class InputAction
    {
        public string PlayerId;
    }

    public class PlayerChoiceAction : InputAction
    {
        public int Choice;
        
        public PlayerChoiceAction(string playerId, int choice)
        {
            PlayerId = playerId;
            Choice = choice;
        }
    }
}