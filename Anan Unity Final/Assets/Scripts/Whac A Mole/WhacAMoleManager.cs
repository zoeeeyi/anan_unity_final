using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class WhacAMoleManager : MonoBehaviour
{
    [Header("Rule Settings")]
    [SerializeField] Vector2 m_possibleTimeBetweenPops;
    [SerializeField] Vector2 m_possibleStayTime;

    [Header("Backend settings")]
    [SerializeField] TextMeshProUGUI m_scoreText;
    [SerializeField] Button[] m_buttonList;
    [SerializeField] Animator[] m_crocdileAnimators;
    AssignedButton[] m_assignedButtons;
    AssignedButton m_chosenButton;
    int m_score = 0;

    #region Whac A Mole State Machine
    enum WAM_States
    {
        Cooldown,
        Play
    }

    WAM_States m_state = WAM_States.Cooldown;
    #endregion

    // Randomly choose a button and change color
    // Random duration for the chosen button
    // If pressed during the chosen duration, score + 1

    void Start()
    {
        m_assignedButtons = new AssignedButton[m_buttonList.Length];
        //Take the list of buttons and construct class objects for them
        for (int i = 0; i < m_buttonList.Length; i++)
        {
            m_assignedButtons[i] = new AssignedButton(m_buttonList[i], m_crocdileAnimators[i]);
        }

        StartCoroutine(ChooseButton());
    }

    #region Game State Managers
    IEnumerator m_currentGame;
    IEnumerator ChooseButton()
    {
        m_state = WAM_States.Cooldown;
        m_chosenButton = null;
        float _waitTime = Random.Range(m_possibleTimeBetweenPops.x, m_possibleTimeBetweenPops.y);
        yield return new WaitForSeconds(_waitTime);

        //Choose a button
        int _buttonIndex = Random.Range(0, m_buttonList.Length);
        m_chosenButton = m_assignedButtons[_buttonIndex];
        m_chosenButton.OnButtonChosen();

        //Wait for input
        m_currentGame = WaitForInput();
        StartCoroutine(m_currentGame);
    }

    IEnumerator WaitForInput()
    {
        m_state = WAM_States.Play;
        float _stayTime = Random.Range(m_possibleStayTime.x, m_possibleStayTime.y);
        yield return new WaitForSeconds(_stayTime);

        //Reset Button
        m_chosenButton.OnButtonDechosen();

        //Start a new round
        StartCoroutine(ChooseButton());
    }
    #endregion

    #region Button Press
    void OnButtonPressed(AssignedButton _pressedButton)
    {
        //Check if the correct button is pressed
        if(_pressedButton.isChosen == true && m_state == WAM_States.Play)
        {
            //Update score
            m_score++;
            m_scoreText.text = m_score.ToString();

            //Set the status of the button
            _pressedButton.OnButtonDechosen(true);

            //Set game state
            StopCoroutine(m_currentGame);
            StartCoroutine(ChooseButton());
        }
    }

    public void OnButtonPressed(int _buttonIndex)
    {
        OnButtonPressed(m_assignedButtons[_buttonIndex]);
    }
    #endregion

    #region Button Class
    class AssignedButton
    {
        public Button assignedButton { get; private set;}
        public bool isChosen {get; private set;}
        public Animator assignedAnimator { get; private set;}
        ColorBlock m_defaultColorBlock;

        public AssignedButton(Button _button, Animator _assignedAnimator)
        {
            assignedButton = _button;
            isChosen = false;
            m_defaultColorBlock = _button.colors;
            assignedAnimator = _assignedAnimator;
        }

        #region Button State Managers
        public void OnButtonChosen()
        {
            isChosen = true;
            //Change button color to white
            assignedButton.TryGetComponent(out Button _button);
            ColorBlock _chosenStatusColor = new ColorBlock();
            _chosenStatusColor.normalColor = Color.white;
            _button.colors = _chosenStatusColor;

            //Play crocdile animation
            assignedAnimator.SetTrigger("Up");
        }

        public void OnButtonDechosen(bool _hit = false)
        {
            isChosen = false;
            assignedButton.colors = m_defaultColorBlock;

            //Play crocdile animation
            assignedAnimator.SetTrigger("Down");
            if (_hit) assignedAnimator.SetTrigger("Hit");
            else assignedAnimator.SetTrigger("Unhit");
        }
        #endregion
    }
    #endregion
}
