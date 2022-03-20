using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class CandleLight : MonoBehaviour
{
    [SerializeField] private float maxIntensity = 0.3f;
    [SerializeField] private float mininIntensity = 0.15f;
    [SerializeField] private float maxRange = 4.0f;
    [SerializeField] private float minRange = 1.0f;
    [SerializeField] private float minChangeSpeed = 1.0f;
    [SerializeField] private float maxChangeSpeed = 2.0f;
    private Light _light;
    public float timer = 0;


    private void Start()
    {
        _light = GetComponent<Light>();
    }


    private void Update()
    {
        timer += Time.deltaTime * 1;

        if (timer > 5)
        {
            _light.intensity = Mathf.Lerp( maxIntensity, mininIntensity, 0.1f);
            timer = 0;
        }


        timer += Time.deltaTime * Random.Range(1f, 2f);

        if (timer > 5)
        {
           // _light.range = Mathf.Lerp(Random.Range(maxRange, minRange), Random.Range(maxRange, minRange), 0.1f);

            timer = 0;
        }
    }
}
