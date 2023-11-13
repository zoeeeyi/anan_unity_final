using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhacAMoleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_scoreText;
    [SerializeField] Button[] m_ButtonList;
    AssignedButton[] m_assignedButtons;


    // Randomly choose a button and change color
    // Random duration for the chosen button
    // If pressed during the chosen duration, score + 1

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_ButtonList.Length; i++)
        {
            m_assignedButtons[i] = new AssignedButton(m_ButtonList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Button Properties
    //Button Properties
    public enum ButtonStates
    {
        Down,
        Up,
    }

    class AssignedButton
    {
        public Button assignedButton;
        public ButtonStates state = ButtonStates.Down;

        public AssignedButton(Button _button)
        {
            assignedButton = _button;
        }
    }
    #endregion
}
