using UnityEngine;

[RequireComponent(typeof(Light))]
public class CandleLight : MonoBehaviour
{
    private float minIntensity = 0.15f;
    private float minChangeSpeed = 1.0f;
    private float maxChangeSpeed = 2.0f;
    private float changeSpeed = 5;
    private float changeSpeed2 = 10;
    private Light _light;
    public float timer = 0;
    [SerializeField] private float moveTimer = 0.005f;
    private Vector3 centrePos;

    private void Start()
    {
        changeSpeed = Random.Range(3, 5);
        changeSpeed2 = Random.Range(5, 10);

        _light = GetComponent<Light>();
        _light.intensity = minIntensity;
        centrePos = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime * Random.Range(minChangeSpeed, maxChangeSpeed);

        if (timer < changeSpeed)
        {
            _light.intensity += Random.Range(0.1f, 0.3f) * Time.deltaTime * 0.1f;
            _light.range += Random.Range(0.1f, 0.3f) * Time.deltaTime * 0.1f;
            _light.gameObject.transform.position += new Vector3(transform.position.x, transform.position.y, transform.position.z) * Time.deltaTime * moveTimer;
        }

        if (timer > changeSpeed)
        {
            _light.intensity -= Random.Range(0.1f, 0.3f) * Time.deltaTime * 0.1f;
            _light.range -= Random.Range(0.1f, 0.3f) * Time.deltaTime * 0.1f;
            _light.gameObject.transform.position -= centrePos * Time.deltaTime * moveTimer;
            if (timer >= changeSpeed2)
                timer = 0;
        }
    }
}
