using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    [HideInInspector] public UnityEvent PlayerDeath;

    protected override void OnDeath()
    {
        PlayerDeath.Invoke();
    }
}
