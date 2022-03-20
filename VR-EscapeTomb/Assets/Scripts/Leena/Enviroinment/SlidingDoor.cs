using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SlidingDoor : MonoBehaviour
{
    [Header("Door Movement Values")]
    public bool open = false;
    [SerializeField] private float openDistance = 2.5f;
    [SerializeField] private float openSpeed = 2f;
    private Vector3 defaultPos;
    private Transform doorPos;
    private AudioSource doorSound;

    void Start()
    {
        open = false;
        defaultPos = transform.position;
        doorPos = gameObject.transform;
        doorSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (open)
            doorPos.position = new Vector3(doorPos.position.x, Mathf.Lerp(doorPos.position.y, defaultPos.y + openDistance, Time.deltaTime * openSpeed), doorPos.position.z); //open up

        else
            doorPos.position = new Vector3(doorPos.position.x, Mathf.Lerp(doorPos.position.y, defaultPos.y, Time.deltaTime * openSpeed), doorPos.position.z); //back to default
    }

    public void MoveDoor()
    {
        open = !open;

        doorSound.Play();
    }

}
