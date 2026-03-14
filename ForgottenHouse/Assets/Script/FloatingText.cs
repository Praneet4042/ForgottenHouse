using UnityEngine;

public class FloatingText : MonoBehaviour
{
    Transform mainCam;

    void LateUpdate()
    {
        if (mainCam == null)
        {
            if (Camera.main != null)
                mainCam = Camera.main.transform;
            return;
        }

        Vector3 dir = transform.position - mainCam.position;
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRot, 0.3f);
        }
    }
}