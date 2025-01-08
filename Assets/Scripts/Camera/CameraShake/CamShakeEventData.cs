using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shake Data", menuName = "Custom Shake Event Data", order = 1)]
public class CamShakeEventData : ScriptableObject
{
    public enum Target
    {
        Position,
        Rotation
    }

    public Target target = Target.Position;

    public float amplitude = 1.0f;
    public float frequency = 1.0f;

    public float duration = 1.0f;

    public AnimationCurve blendOverLifetime = new AnimationCurve(

        new Keyframe(0.0f, 0.0f, Mathf.Deg2Rad * 0.0f, Mathf.Deg2Rad * 720.0f),
        new Keyframe(0.2f, 2.0f),
        new Keyframe(1.0f, 0.0f));

    public void Init(float amplitude, float frequency, float duration, AnimationCurve blendOverLifetime, Target target)
    {
        this.target = target;

        this.amplitude = amplitude;
        this.frequency = frequency;

        this.duration = duration;

        this.blendOverLifetime = blendOverLifetime;
    }
}
