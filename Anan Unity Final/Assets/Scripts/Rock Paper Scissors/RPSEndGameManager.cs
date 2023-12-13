using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RPSEndGameManager : MonoBehaviour
{
    PlayableDirector m_directorEndGame;

    void Start()
    {
        m_directorEndGame = GetComponent<PlayableDirector>();

        RPSCore.instance.EndGameMaster.AddListener(delegate { StartCoroutine(SlowDownAnimation()); });
        RPSCore.instance.CompDeath.AddListener(PlayEndGameTimeline);
    }

    void PlayEndGameTimeline()
    {
        m_directorEndGame.Play();
    }

    #region Slow Down Animation and Timeline
    [Header("Slow Down Animation and Timeline")]
    [SerializeField] PlayableDirector m_directorCore;
    [SerializeField] Animator m_playerAnimator;
    [SerializeField] Animator m_compAnimator;

    //Smooth damp settings
    [Space]
    [SerializeField] float m_slowSmoothTime = 0.5f;
    float m_currentPlaySpeed = 1;
    float m_slowSmoothV;

    IEnumerator SlowDownAnimation()
    {
        while (true)
        {
            m_currentPlaySpeed = Mathf.SmoothDamp(m_currentPlaySpeed, 0, 
                ref m_slowSmoothV, m_slowSmoothTime);

            //If playback speed reaches 0, stop animations and timeline.
            if (m_currentPlaySpeed <= 0)
            {
                m_directorCore.Stop();
                m_playerAnimator.StopPlayback();
                m_compAnimator.StopPlayback();
                break;
            }

            //Set timeline and animation speed
            m_directorCore.playableGraph.GetRootPlayable(0).SetSpeed(m_currentPlaySpeed);
            m_playerAnimator.speed = m_currentPlaySpeed;
            m_compAnimator.speed = m_currentPlaySpeed;

            yield return null;
        }
    }
    #endregion
}
