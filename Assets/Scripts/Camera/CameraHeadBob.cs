using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadBob : MonoBehaviour
{
    [Header("View Bob Settings")]
    [SerializeField] private bool enabled = true;
    [SerializeField] private Vector2 viewBobMultiplier;
    [SerializeField] private float viewBobSpeed;
    [SerializeField] private float viewBobDampingRatio;
    [SerializeField] private float viewBobAngularFrequency;
    [Space(10)]
    [SerializeField] private float maxTilt;
    [SerializeField] private float tiltSmoothTime;
    private float viewBobTimer = 0f;
    private float landBobOffset = 0f;

    private Vector3 bobVel = Vector3.zero;
    private float tiltVel = 0f;

    public bool Bobbing => viewBobTimer != 0f;

    public float TiltSway { get; private set; }
    public Vector3 ViewBobOffset { get; private set; }
    public Vector3 ViewBobSnapOffset { get; private set; }

    public void BobUpdate(PlayerMovement player, PlayerCam playerCam)
    {
        if (!enabled) return;

        // Update view bob timer based on player movement
        if (player.grounded && player.moveDirection.magnitude > 0.5f)
        {
            viewBobTimer += Time.deltaTime * viewBobSpeed;
        }
        else
        {
            viewBobTimer = 0f;
        }

        // Reset landBobOffset smoothly over time when not landing
        if (player.grounded)
        {
            landBobOffset = Mathf.Lerp(landBobOffset, 0f, Time.deltaTime * 10f);
        }

        // Calculate head bob spring motion
        ViewBobSnapOffset = HeadBobOffset(viewBobTimer) + Vector3.down * landBobOffset;
        Vector3 smoothHeadBob = ViewBobOffset;

        HarmonicMotion.Calculate(ref smoothHeadBob, ref bobVel, ViewBobSnapOffset,
            HarmonicMotion.CalcDampedSpringMotionParams(viewBobDampingRatio, viewBobAngularFrequency));
        ViewBobOffset = smoothHeadBob;

        // Calculate tilt sway based on player input and camera rotation
        float tilt = Mathf.Clamp(
            (player.horizontalInput * maxTilt * 0.75f) + (playerCam.sensX * playerCam.orientation.localEulerAngles.y * maxTilt),
            -maxTilt,
            maxTilt
        );
        TiltSway = Mathf.SmoothDamp(TiltSway, -tilt, ref tiltVel, tiltSmoothTime);
    }

    private Vector3 HeadBobOffset(float timer)
    {
        if (timer <= 0) return Vector3.zero;
        return new Vector3(viewBobMultiplier.x * Mathf.Cos(timer), viewBobMultiplier.y * Mathf.Abs(Mathf.Sin(timer)), 0f);
    }

    public void BobOnce(float magnitude)
    {
        if (!enabled) return;

        landBobOffset -= magnitude;
    }

    public void Enable(bool isEnabled) => enabled = isEnabled;
}
