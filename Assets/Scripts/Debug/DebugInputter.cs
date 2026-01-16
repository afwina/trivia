using System.Collections;
using System.Collections.Generic;
using Match.Input;
using TMPro;
using UnityEngine;

public class DebugInputter : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown m_PlayerDropdown;
    [SerializeField] private InputController m_InputController;

    public void OnInputPressed(int input)
    {
        m_InputController.SetPlayerChoice(m_PlayerDropdown.options[m_PlayerDropdown.value].text, input);
    }
}
