using SmoothShakeScript;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

[RequireComponent(typeof(SmoothShake))]
public class CameraShakeController : MonoBehaviour
{
    [SerializeField] Preset m_playerWinPreset;
    [SerializeField] Preset m_compWinPreset;

    SmoothShake m_smoothShake;

    private void Start()
    {
        m_smoothShake = GetComponent<SmoothShake>();
        RPSCore.instance.AnimateCompWin.AddListener(OnCompWinCamShake);
        RPSCore.instance.AnimatePlayerWin.AddListener(OnPlayerWinCamShake);
    }

    void OnPlayerWinCamShake()
    {
        m_playerWinPreset.ApplyTo(m_smoothShake);
        OnCameraShake();
    }

    void OnCompWinCamShake()
    {
        m_compWinPreset.ApplyTo(m_smoothShake);
        OnCameraShake();
    }

    void OnCameraShake()
    {
        m_smoothShake.StartShake();
    }
}
