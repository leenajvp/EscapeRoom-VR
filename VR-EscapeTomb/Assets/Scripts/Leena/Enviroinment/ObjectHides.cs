using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjectHides : MonoBehaviour
{
    public enum MoveDir { xPlus, zPlus, xMinus, zMinus }
    public bool unlocked = false;
    public bool open = false;

    [Header("Movement Values")]
    public MoveDir moveAxis;
    [SerializeField] private float moveBackAmount = 0.1f;
    [SerializeField] private float moveUpkAmount = 0.6f;
    [SerializeField] private float openSpeed = 0.1f;
    private Vector3 defaultPos;
    private Transform currentPos;
    private float openTime = 0f;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip moveBack = null;
    [SerializeField] private AudioClip moveUp = null;

    private bool movingXZ;
    private bool movingY;

    private void Start()
    {
        defaultPos = transform.position;
        currentPos = gameObject.transform;

        //NESSIE - AUDIO
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Sounds();
        if (open)
        {
            openTime += Time.deltaTime * openSpeed;

            if (moveAxis == MoveDir.xMinus)
                currentPos.position = new Vector3(Mathf.Lerp(currentPos.position.x, defaultPos.x - moveBackAmount, Time.deltaTime * openSpeed), currentPos.position.y, currentPos.position.z); // move back x

            if (moveAxis == MoveDir.xPlus)
                currentPos.position = new Vector3(Mathf.Lerp(currentPos.position.x, defaultPos.x + moveBackAmount, Time.deltaTime * openSpeed), currentPos.position.y, currentPos.position.z); // move back x

            if (moveAxis == MoveDir.zMinus)
                currentPos.position = new Vector3(currentPos.position.x, currentPos.position.y, Mathf.Lerp(currentPos.position.z, defaultPos.z - moveBackAmount, Time.deltaTime * openSpeed)); // move back z

            if (moveAxis == MoveDir.zPlus)
                currentPos.position = new Vector3(currentPos.position.x, currentPos.position.y, Mathf.Lerp(currentPos.position.z, defaultPos.z + moveBackAmount, Time.deltaTime * openSpeed)); // move back z

            movingXZ = true;

            if (openTime >= 1.0f)
            {
                currentPos.position = new Vector3(currentPos.position.x, Mathf.Lerp(currentPos.position.y, defaultPos.y + moveUpkAmount, Time.deltaTime * openSpeed), currentPos.position.z); // move up
                movingXZ = false;
                movingY = true;
            }
        }
    }

    private void Sounds()
    {
        if (movingXZ && !movingY)
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(moveBack);
        }

        else if (movingY && !movingXZ)
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(moveUp);

            movingY = false;
        }

        if (!movingXZ && !movingY)
            audioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (unlocked && collision.gameObject.tag == "Player")
            open = true;
    }
}
