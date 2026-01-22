using System.Collections;
using System.Collections.Generic;
using Game.Input;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DebugInputter : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown m_PlayerDropdown;
    [SerializeField] private ClientsideInputController m_InputController;

    public void OnInputPressed(int input)
    {
        m_InputController.SetPlayerChoice(m_PlayerDropdown.options[m_PlayerDropdown.value].text, input);
    }
}
