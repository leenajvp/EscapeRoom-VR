using System.Collections;
using UnityEngine;

namespace ButtonPuzzle
{
    public class Buttons : MonoBehaviour
    {
        [Header("Button values")]
        [Tooltip("Object to manage the buttons")]
        [SerializeField] private ButtonPuzzleManager manager;
        //[SerializeField] string orderNumber = "1";
        [SerializeField] public int num = 1;
        [Tooltip("Speed that the button returns to default position")]
        [SerializeField] float resetSpeed = 1.0f;
        [HideInInspector] public bool reset = false;
        public bool pressed = false;
        private float timer = 0;
        private Vector3 defaultPos;
        private Rigidbody rb;
        private AudioSource audioSource;
        public GameObject trigger;
        float pressTimer = 0;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody>();
            pressed = false;
            reset = false;
            defaultPos = transform.position;

            if (manager == null) try { manager = transform.parent.GetComponent<ButtonPuzzleManager>(); } catch { Debug.LogError(name + "Manager is null"); }
        }

        void Update()
        {
            if (reset)
            {
                rb.isKinematic = false;
                timer += Time.deltaTime * 0.5f;

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, defaultPos.x, Time.deltaTime * resetSpeed), transform.position.y, Mathf.Lerp(transform.position.z, defaultPos.z, Time.deltaTime * resetSpeed));

                if (timer >= 1.5f)
                {
                  //  rb.isKinematic = false;
                    reset = false;
                    timer = 0;
                }
            }
        }

        public void Sound()
        {
            audioSource.Play();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject == trigger && !pressed)
            {
                rb.isKinematic = true;
                manager.AddNumber(num);
                pressTimer = 0;
                pressed = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject == trigger && pressed)
            {
                pressed = false;
            }

        }
    }
}

