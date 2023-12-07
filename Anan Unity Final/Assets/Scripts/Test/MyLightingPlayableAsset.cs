using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "MyLightingPlayableAsset", menuName = "Timeline/MyLightingPlayableAsset")]
public class MyLightingPlayableAsset : PlayableAsset
{
    public Color startColor = Color.white;
    public Color endColor = Color.blue;
    public float startIntensity = 1f;
    public float endIntensity = 0f;
    public ExposedReference<Light> light;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MyLightingPlayable>.Create(graph);
        MyLightingPlayable myLightingPlayable = playable.GetBehaviour();
        myLightingPlayable.startColor = startColor;
        myLightingPlayable.endColor = endColor;
        myLightingPlayable.startIntensity = startIntensity;
        myLightingPlayable.endIntensity = endIntensity;
        myLightingPlayable.Initialize(light.Resolve(graph.GetResolver()));
        return playable;
    }
}
