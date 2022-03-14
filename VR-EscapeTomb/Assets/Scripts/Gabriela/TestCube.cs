using UnityEngine;

public class TestCube : MonoBehaviour
{
    public float speed = 1f;
    public Color startColor;
    public Color endColor;
    float startTime;
    public bool repeat;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (repeat)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            float t = Mathf.Sin(Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }


    }
}
