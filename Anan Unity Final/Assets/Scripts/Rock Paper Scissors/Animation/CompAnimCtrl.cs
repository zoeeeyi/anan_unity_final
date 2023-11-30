using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CompAnimCtrl : MonoBehaviour
{
    Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        RPSCore.StartRound.AddListener(delegate { PlayAnim(RPSCore.compHand); }) ;
    }
    
    void PlayAnim(RPSCore.Hand _hand)
    {
        switch (_hand)
        {

        }
    }
}
