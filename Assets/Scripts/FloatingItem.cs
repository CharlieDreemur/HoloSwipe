using UnityEngine;
using DG.Tweening;

public class FloatingItem : MonoBehaviour
{
    [Header("Floating Settings")]
    [Tooltip("Height difference for floating movement")]
    public float amplitude = 0.5f;
    [Tooltip("Time in seconds for one complete up-down cycle")]
    public float cycleDuration = 2f;

    [Header("Rotation Settings")]
    [Tooltip("Enable 360° continuous rotation")]
    public bool enableRotation = true;
    [Tooltip("Rotation axis (world space)")]
    public Vector3 rotationAxis = Vector3.up;
    [Tooltip("Time in seconds for one complete 360° rotation")]
    public float rotationDuration = 3f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Tween floatTween;
    private Tween rotateTween;

    private void OnEnable()
    {
        // Store initial transform values
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Start both animations
        StartFloating();
        StartRotation();
    }

    private void OnDisable()
    {
        // Stop and reset both animations
        StopFloating();
        StopRotation();
    }

    void StartFloating()
    {
        floatTween = transform.DOMoveY(initialPosition.y + amplitude, cycleDuration / 2)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .OnKill(() => transform.position = initialPosition);
    }

    void StartRotation()
    {
        if (!enableRotation) return;

        rotateTween = transform.DORotate(rotationAxis * 360, rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetRelative()  // Rotate relative to current rotation
            .SetLoops(-1, LoopType.Restart)
            .OnKill(() => transform.rotation = initialRotation);
    }

    void StopFloating()
    {
        floatTween?.Kill();
    }

    void StopRotation()
    {
        rotateTween?.Kill();
    }
}