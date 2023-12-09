using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingRing : MonoBehaviour
{
    Animator m_animator;

    [SerializeField] RPSCore m_rpsCore;
    [SerializeField] DamageSystem m_damageSystem;
    bool m_timeRunOut;

    [Header("Result Particles")]
    [SerializeField] ParticleSystem m_perfectParticle;
    [SerializeField] ParticleSystem m_goodParticle;
    [SerializeField] ParticleSystem m_greatParticle;
    [SerializeField] ParticleSystem m_failParticle;

    private void Start()
    {
        m_animator = GetComponent<Animator>();

        RPSCore.instance.StartRound.AddListener(EnableAnimator);
        RPSCore.instance.PlayerSelect.AddListener(PauseRing);
    }

    #region Animation Events
    public void InitializeRing()
    {
        m_timeRunOut = true;
        m_rpsCore.OnWaitPlayerInput();
        m_damageSystem.ChangePlayerDamage(DamageSystem.PlayerDamageStage.Min);
    }

    public void SetPlayerDamage(DamageSystem.PlayerDamageStage _stage)
    {
        m_damageSystem.ChangePlayerDamage(_stage);
    }

    public void CloseRing()
    {
        if (m_timeRunOut)
        {
            m_failParticle.Stop();
            m_failParticle.Play();
            m_rpsCore.OnEndRound(true);
        }
    }
    #endregion

    #region State Machine
    public void PauseRing()
    {
        m_timeRunOut = false;
        m_animator.SetTrigger("Pause");

        //Set result particles
        switch (m_damageSystem.currentDamageStage)
        {
            case DamageSystem.PlayerDamageStage.Min:
                m_goodParticle.Stop();
                m_goodParticle.Play();
                break;

            case DamageSystem.PlayerDamageStage.Stage1:
                m_greatParticle.Stop();
                m_greatParticle.Play();
                break;

            case DamageSystem.PlayerDamageStage.Max:
                m_perfectParticle.Stop();
                m_perfectParticle.Play();
                break;
        }
    }

    public void EnableAnimator()
    {
        m_animator.enabled = true;
    }

    public void DisableAnimator()
    {
        m_animator.enabled = false;
    }
    #endregion
}
