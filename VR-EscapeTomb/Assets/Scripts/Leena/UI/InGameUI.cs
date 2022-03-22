using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public enum Functions { GetClue, QuitGame };
    [Header("Button Function")]
    public Functions function;

    [Header("Button Settings")]
    [SerializeField] private GameObject trigger;
    [Tooltip("Speed that the button returns to default position")]
    [SerializeField] float resetSpeed = 2.0f;
    [Tooltip("Time that function is cancelled")]
    [SerializeField] float resetTime = 60;

    [Header("Quit Game Settings")]
    [SerializeField] private GameObject exitDoor;
    [SerializeField] private GameObject exitTeleportation;

    [Header("Clue Settings")]
    public int currentClue;
    [SerializeField] private List<Hints> quest1Clues = new List<Hints>();
    [SerializeField] private List<Hints> quest2Clues = new List<Hints>();
    [SerializeField] private List<Hints> quest3Clues = new List<Hints>();
    [SerializeField] private List<Hints> quest4Clues = new List<Hints>();
    [SerializeField] private List<Hints> quest5Clues = new List<Hints>();

    private bool reset = false;
    private bool pressed = false;
    private float mTimer = 0;
    private float rTimer = 0;
    private Vector3 defaultPos;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reset = false;
        defaultPos = transform.position;
        exitTeleportation.SetActive(false);
    }

    void Update()
    {
        if (reset)
        {
            rb.isKinematic = true;
            mTimer += Time.deltaTime * 0.5f;

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, defaultPos.x, Time.deltaTime * resetSpeed), transform.position.y, Mathf.Lerp(transform.position.z, defaultPos.z, Time.deltaTime * resetSpeed));

            if (mTimer >= 1f)
            {
                rb.isKinematic = false;
                reset = false;
                mTimer = 0;
            }
        }

        //if (rTimer > resetTime)
        //{
        //    reset = true;

        //    if (function == Functions.GetClue)
        //    {
        //        //clue set inactive
        //    }

        //    else
        //    {
        //        exitDoor.GetComponent<SlidingDoor>().open = false;
        //        exitTeleportation.SetActive(false);
        //        rTimer = 0;
        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == trigger && !pressed)
        {
            rb.isKinematic = true;
            pressed = true;

            if (function == Functions.GetClue)
            {

            }

            else
            {
                ExitGame();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == trigger && pressed)
        {
            pressed = false;
        }
    }

    private void ExitGame()
    {
        Debug.Log("exit");
        exitDoor.GetComponent<SlidingDoor>().open = true;
        exitTeleportation.SetActive(true);
        rTimer = 0;
        rb.isKinematic = true;
       // reset = true;
    }
}

