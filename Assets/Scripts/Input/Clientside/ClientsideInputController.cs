using System;
using UnityEngine;

namespace Game.Input
{
    public class ClientsideInputController : MonoBehaviour, IInputController
    {
        public Action<AInputAction>  OnInputAction { get; set; }

        public void SetPlayerChoice(string playerId, int choice)
        {
            OnInputAction.Invoke(new PlayerChoiceAction(playerId, choice));
        }
    }
}