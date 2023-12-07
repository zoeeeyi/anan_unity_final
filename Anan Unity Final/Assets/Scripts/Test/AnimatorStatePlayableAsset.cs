using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "AnimatorStatePlayableAsset", menuName = "Timeline/AnimatorStatePlayableAsset")]
public class AnimatorStatePlayableAsset : PlayableAsset
{
    public ExposedReference<Animator> animator;
    public string stateName;
    public float normalizedTime = 0;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AnimatorStatePlayable>.Create(graph);
        AnimatorStatePlayable animatorStatePlayable = playable.GetBehaviour();

        animatorStatePlayable.animator = animator.Resolve(graph.GetResolver());
        animatorStatePlayable.stateName = stateName;
        animatorStatePlayable.normalizedTime = normalizedTime;

        return playable;
    }
}
