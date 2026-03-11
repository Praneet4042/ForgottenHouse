using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GhostAudioController : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip idleLoop;
    public AudioClip chaseLoop;

    [Header("Distances")]
    public float chaseRange = 60f;
    public float silentRange = 200f;

    [Header("Switch Cooldown")]
    public float switchCooldown = 4f; // seconds before sound can switch again

    [Header("Debug")]
    public bool showDebug = true;

    private AudioSource _src;
    private Transform _player;
    private float _currentDist;
    private bool _inChaseZone = false;
    private float _lastSwitchTime = -99f;

    void Start()
    {
        _src = GetComponent<AudioSource>();
        _src.loop = true;
        _src.spatialBlend = 1f;
        _src.rolloffMode = AudioRolloffMode.Linear;
        _src.maxDistance = silentRange;
        _src.playOnAwake = false;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) _player = playerObj.transform;
        else Debug.LogError("GhostAudio: Player tag not found!");

        _src.clip = idleLoop;
        _src.Play();
    }

    void Update()
    {
        if (_player == null) return;

        _currentDist = Vector3.Distance(transform.position, _player.position);

        // Volume
        float t = 1f - Mathf.Clamp01(_currentDist / silentRange);
        _src.volume = t * t;

        // Only allow switching if cooldown has passed
        if (Time.time - _lastSwitchTime < switchCooldown) return;

        bool shouldChase = _currentDist <= chaseRange;

        if (shouldChase && !_inChaseZone)
        {
            _inChaseZone = true;
            SwitchTo(chaseLoop);
        }
        else if (!shouldChase && _inChaseZone)
        {
            _inChaseZone = false;
            SwitchTo(idleLoop);
        }
    }

    void SwitchTo(AudioClip clip)
    {
        if (_src.clip == clip) return;
        _src.clip = clip;
        _src.Play();
        _lastSwitchTime = Time.time;
    }

    void OnGUI()
    {
        if (!showDebug) return;
        float cooldownLeft = Mathf.Max(0, switchCooldown - (Time.time - _lastSwitchTime));
        GUI.color = Color.red;
        GUI.Label(new Rect(10, 10, 400, 25), $"Ghost Dist: {_currentDist:F1}  |  Zone: {(_inChaseZone ? "CHASE" : "IDLE")}");
        GUI.Label(new Rect(10, 35, 400, 25), $"Switch cooldown remaining: {cooldownLeft:F1}s");
        GUI.color = Color.white;
    }
}