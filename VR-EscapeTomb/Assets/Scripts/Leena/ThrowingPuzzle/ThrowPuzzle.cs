using UnityEngine;

public class ThrowPuzzle : MonoBehaviour
{
    public bool completed;
    public float speed = 0.01f;
    [SerializeField] Transform spikes;

    private void Update()
    {
        if (completed)
           spikes.position = new Vector3(spikes.position.x, Mathf.Lerp(spikes.position.y, spikes.position.y - 2, Time.deltaTime * speed), spikes.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        ThrowBalls ball = other.gameObject.GetComponent<ThrowBalls>();

        if (ball)
            completed = true;
    }
}
