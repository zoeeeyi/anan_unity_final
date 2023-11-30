using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RPSCore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Core Game Logic
    bool m_idle = true;
    public static UnityEvent StartRound;
    public static UnityEvent EndRound;
    public static UnityEvent PlayerSelect;
    public static UnityEvent PlayerWin;
    public static UnityEvent CompWin;
    public static UnityEvent Draw;

    public void OnStartRound()
    {
        SetCompHand();
        StartRound.Invoke();
    }

    public void OnEndRound()
    {
        if (m_idle) return;
        m_idle = true;
    }

    public void OnWaitPlayerInput()
    {
        m_idle = false;
    }

    void OnPlayerHandSelect(Hand _hand)
    {
        if (m_idle) return;

        int _result = CompareHands(_hand, compHand);
        switch (_result)
        {
            case 0:
                OnPlayerWin();
                break;

            case 1:
                OnCompWin();
                break;

            case 2:
                OnDraw();
                break;
        }

        OnEndRound();
        PlayerSelect.Invoke();
    }

    void OnPlayerWin()
    {
        PlayerWin.Invoke();
    }

    void OnCompWin()
    {
        CompWin.Invoke();
    }

    void OnDraw()
    {
        Draw.Invoke();
    }
    #endregion

    #region Rock Paper Scissors Rules
    public static Hand playerHand { get; private set; }
    public static Hand compHand { get; private set; }

    public enum Hand
    {
        Rock,
        Paper,
        Scissors
    }

    void SetCompHand()
    {
        //Randomly choose a hand
        Array _hands = Enum.GetValues(typeof(Hand));
        System.Random _random = new System.Random();
        compHand = (Hand) _hands.GetValue(_random.Next(_hands.Length));
    }

    #region Button Press
    public void OnRockPressed()
    {
        OnPlayerHandSelect(Hand.Rock);
    }

    public void OnScissorsPressed()
    {
        OnPlayerHandSelect(Hand.Scissors);
    }

    public void OnPaperPressed()
    {
        OnPlayerHandSelect(Hand.Paper);
    }
    #endregion

    int CompareHands(Hand _playerHand, Hand _compHand)
    {
        switch(_playerHand)
        {
            case Hand.Rock:
                if (_compHand == Hand.Scissors) return 0;
                if (_compHand == Hand.Paper) return 1;
                break;

            case Hand.Scissors:
                if (_compHand == Hand.Paper) return 0;
                if (_compHand == Hand.Rock) return 1;
                break;

            case Hand.Paper:
                if (_compHand == Hand.Rock) return 0;
                if (_compHand == Hand.Scissors) return 1;
                break;
        }

        //if they have same hands, return 2 as draw
        return 2;
    }
    #endregion
}
