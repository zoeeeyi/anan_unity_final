using UnityEngine;
using UnityEngine.Playables;

public class MyLightingPlayable : PlayableBehaviour
{
    public Color startColor;
    public Color endColor;
    public float startIntensity;
    public float endIntensity;
    private Light targetLight;

    public void Initialize(Light light)
    {
        targetLight = light;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (targetLight != null)
        {
            float progress = (float)(playable.GetTime() / playable.GetDuration());
            targetLight.color = Color.Lerp(startColor, endColor, progress);
            targetLight.intensity = Mathf.Lerp(startIntensity, endIntensity, progress);
        }
    }
}
