using UnityEngine;

public class LightPulse : MonoBehaviour
{
    private Light pulseLight;
    public float pulseSpeed = 2.0f;
    public float baseIntensity = 2f;
    public float pulseRange = 1f;

    void Start()
    {
        pulseLight = GetComponent<Light>();
    }

    void Update()
    {
        pulseLight.intensity = baseIntensity + Mathf.Sin(Time.time * pulseSpeed) * pulseRange;
    }
}
