using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour
{
    Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        RPSCore.PlayerSelect.AddListener(delegate { PlayAnim(RPSCore.playerHand); });
    }

    void PlayAnim(RPSCore.Hand _hand)
    {
        switch (_hand)
        {

        }
    }
}
