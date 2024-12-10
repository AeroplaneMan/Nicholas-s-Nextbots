using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeTransform : MonoBehaviour
{
    public class ShakeEvent {
        float duration;
        float timeRemaning;

        CamShakeEventData data;

        public CamShakeEventData.Target target
        {
            get
            {
                return data.target;
            }
        }

        Vector3 noiseOffset;
        public Vector3 noise;

        public ShakeEvent(CamShakeEventData data)
        {
            this.data = data;
            duration = data.duration;
            timeRemaning = duration;

            float random = 32.0f;

            noiseOffset.x = Random.Range(0.0f, random);
            noiseOffset.y = Random.Range(0.0f, random);
            noiseOffset.z = Random.Range(0.0f, random);
        }

        public void Update()
        {
            float deltaTime = Time.deltaTime;

            timeRemaning -= deltaTime;

            float noiseOffestDelta = deltaTime * data.frequency;

            noiseOffset.x = noiseOffestDelta;
            noiseOffset.y = noiseOffestDelta;
            noiseOffset.z = noiseOffestDelta;

            noise.x = Mathf.PerlinNoise(noiseOffset.x, 0.0f);
            noise.y = Mathf.PerlinNoise(noiseOffset.y, 1.0f);
            noise.z = Mathf.PerlinNoise(noiseOffset.z, 2.0f);

            noise -= Vector3.one * 0.5f;

            noise *= data.amplitude;

            float agePercent = 1.0f - (timeRemaning / duration);
            noise *= data.blendOverLifetime.Evaluate(agePercent);
        }

        public bool IsAlive()
        {
            return timeRemaning > 0.0f;
        }
    }

    List<ShakeEvent> shakeEvents = new List<ShakeEvent>();

    public void AddShakeEvent(CamShakeEventData data)
    {
        shakeEvents.Add(new ShakeEvent(data));
    }

    public void AddShakeEvent(float amplitude, float frequency, float duration, AnimationCurve blendOverLifetime, CamShakeEventData.Target target)
    {
        CamShakeEventData data = ScriptableObject.CreateInstance<CamShakeEventData>();
        data.Init(amplitude, frequency, duration, blendOverLifetime, target);

        AddShakeEvent(data);
    }

    void LateUpdate()
    {
        Vector3 positionOffset = Vector3.zero;
        Vector3 rotationOffset = Vector3.zero;

        for (int i = shakeEvents.Count - 1; i >= 0; i--)
        {
            ShakeEvent se = shakeEvents[i]; se.Update();

            if (se.target == CamShakeEventData.Target.Position)
            {
                positionOffset += se.noise;
            }
            else
            {
                rotationOffset += se.noise;
            }

            if (!se.IsAlive()) shakeEvents.RemoveAt(i);
        }

        transform.localPosition = positionOffset;
        transform.localEulerAngles = rotationOffset;
    }
}
