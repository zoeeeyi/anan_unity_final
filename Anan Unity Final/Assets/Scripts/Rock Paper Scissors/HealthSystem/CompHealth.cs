using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompHealth : Health
{
    [HideInInspector] public UnityEvent CompDeath;

    protected override void OnDeath()
    {
        CompDeath.Invoke();
    }
}
