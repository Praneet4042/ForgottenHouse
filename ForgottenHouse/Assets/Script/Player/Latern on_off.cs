using UnityEngine;

public class LanternToggle : MonoBehaviour
{
    public Light lanternLight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            lanternLight.enabled = !lanternLight.enabled;
        }
    }
}