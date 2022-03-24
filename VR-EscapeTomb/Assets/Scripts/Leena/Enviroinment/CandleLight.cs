using UnityEngine;

[RequireComponent(typeof(Light))]
public class CandleLight : MonoBehaviour
{
    [Header("Candle Light values")]
    [SerializeField] private float maxIntensity = 0.3f;
    [SerializeField] private float minRange = 0.3f;
    [Tooltip("Speed that light values increase")]
    [SerializeField] private float changeSpeed = 5;
    [Tooltip("Time between increase/decrease")]
    [SerializeField] private float changeTime = 5;

    private float maxRange, minIntensity;
    private bool increase = true;
    private Light _light;

    private void Start()
    {
        _light = GetComponent<Light>();
        increase = true;
        minIntensity = _light.intensity;
        maxRange = _light.range;

        InvokeRepeating("Change", 0, changeTime);
    }

    private void Update()
    {
        if (increase)
        {
            if (_light.intensity < maxIntensity)
                _light.intensity += 0.1f * Time.deltaTime * changeSpeed;

            if (_light.range < maxRange)
                _light.range += 0.1f * Time.deltaTime * changeSpeed;
        }

        if (!increase)
        {
            if (_light.intensity > minIntensity)
                _light.intensity -= 0.1f * Time.deltaTime * changeSpeed;

            if (_light.range > minRange)
                _light.range -= 0.1f * Time.deltaTime * changeSpeed;
        }
    }

    private void Change()
    {
        increase = !increase;
    }
}
