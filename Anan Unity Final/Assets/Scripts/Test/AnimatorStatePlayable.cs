using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class AnimatorStatePlayable : PlayableBehaviour
{
    public Animator animator;
    public string stateName;
    public float normalizedTime;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (animator != null)
        {
            animator.Play(stateName, 0, normalizedTime);
        }
    }
}