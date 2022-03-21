using UnityEngine;

namespace ButtonPuzzle
{
    public class Buttons : MonoBehaviour
    {
        [Header("Button values")]
        [Tooltip("Object to manage the buttons")]
        [SerializeField] private ButtonPuzzleManager manager;
        [SerializeField] string orderNumber = "1";
        [Tooltip("Speed that the button returns to default position")]
        [SerializeField] float resetSpeed = 1.0f;
        [HideInInspector] public bool reset = false;
        private bool pressed = false;
        private float timer = 0;
        private Vector3 defaultPos;
        private Rigidbody rb;

        void Start()
        {
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
                rb.isKinematic = true;
                timer += Time.deltaTime * 0.5f;

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, defaultPos.x, Time.deltaTime * resetSpeed), transform.position.y, Mathf.Lerp(transform.position.z, defaultPos.z, Time.deltaTime * resetSpeed));

                if (timer >= 1f)
                {
                    rb.isKinematic = false;
                    reset = false;
                    timer = 0;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!pressed)
            {
               // rb.isKinematic = true;
                manager.AddNumber(orderNumber);
                pressed = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            pressed = false;
        }
    }
}

