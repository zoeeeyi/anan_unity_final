using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBattleVFX : MonoBehaviour
{
    [SerializeField] ParticleSystem m_playerAttackVFX;
    [SerializeField] ParticleSystem m_compAttackVFX;

    void PlayPlayerAttackVFX()
    {
        m_playerAttackVFX.Stop();
        m_playerAttackVFX.Play();
    }

    void PlayCompAttackVFX()
    {
        m_compAttackVFX.Stop();
        m_compAttackVFX.Play();
    }
}
