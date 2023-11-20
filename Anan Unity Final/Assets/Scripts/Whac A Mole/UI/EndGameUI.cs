using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] WhacAMoleManager m_gm;

    [Space]
    [SerializeField] GameObject m_restartUI;
    [SerializeField] GameObject m_gameOverUI;
    private void Awake()
    {
        m_gm.WAM_Win.AddListener(ActivateGameOverUI);
        m_gm.WAM_Lose.AddListener(ActivateRestartUI);
    }

    private void Start()
    {
        m_restartUI.SetActive(false);
        m_gameOverUI.SetActive(false);
    }

    void ActivateRestartUI()
    {
        m_restartUI.SetActive(true);
    }

    void ActivateGameOverUI()
    {
        m_gameOverUI.SetActive(true);
    }

}
