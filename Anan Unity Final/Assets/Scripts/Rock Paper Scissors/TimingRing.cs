using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingRing : MonoBehaviour
{
    Animator m_animator;

    [SerializeField] RPSCore m_rpsCore;
    [SerializeField] DamageSystem m_damageSystem;

    private void Start()
    {
        m_animator = GetComponent<Animator>();

        RPSCore.instance.StartRound.AddListener(EnableAnimator);
        RPSCore.instance.PlayerSelect.AddListener(PauseRing);
    }

    #region Animation Events
    public void InitializeRing()
    {
        m_rpsCore.OnWaitPlayerInput();
        m_damageSystem.ChangePlayerDamage(DamageSystem.PlayerDamageStage.Min);
    }

    public void SetPlayerDamage(DamageSystem.PlayerDamageStage _stage)
    {
        m_damageSystem.ChangePlayerDamage(_stage);
    }

    public void CloseRing()
    {
        m_rpsCore.OnEndRound(true);
    }
    #endregion

    #region State Machine
    public void StartTimingRing(float _playSpeed)
    {

    }

    public void PauseRing()
    {
        m_animator.SetTrigger("Pause");
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
