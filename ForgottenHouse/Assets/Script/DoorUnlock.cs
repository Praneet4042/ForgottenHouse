using System.Collections;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public enum RotationAxis { X, Y, Z }
    public RotationAxis rotationAxis = RotationAxis.Y;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip doorCreakClip;

    private bool _isOpen = false;
    private bool _isOpening = false;

    public void UnlockDoor()
    {
        if (_isOpen || _isOpening) return;
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        _isOpening = true;

        if (audioSource != null && doorCreakClip != null)
            audioSource.PlayOneShot(doorCreakClip);

        Quaternion startRot = transform.rotation;

        Vector3 euler = rotationAxis switch
        {
            RotationAxis.X => new Vector3(openAngle, 0, 0),
            RotationAxis.Y => new Vector3(0, openAngle, 0),
            RotationAxis.Z => new Vector3(0, 0, openAngle),
            _ => new Vector3(0, openAngle, 0)
        };

        Quaternion endRot = startRot * Quaternion.Euler(euler);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        transform.rotation = endRot;
        _isOpen = true;
        _isOpening = false;
    }
}