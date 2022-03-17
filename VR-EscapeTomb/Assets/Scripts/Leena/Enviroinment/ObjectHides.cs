using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHides : MonoBehaviour
{
    public bool unlocked = false;
    private bool open = false;

    [Header("Movement Values")]
    [SerializeField] private float moveBackAmount = 0.1f;
    [SerializeField] private float moveUpkAmount = 0.6f;
    [SerializeField] private float openSpeed = 0.1f;
    private Vector3 defaultPos;
    private Transform currentPos;

    [Header("Sound Effects")]
    [SerializeField]private AudioSource moveBack;
    [SerializeField]private AudioSource moveUp;
    private float openTime = 0f;

    private void Start()
    {
        defaultPos = transform.position;
        currentPos = gameObject.transform;
    }

    private void Update()
    {
        if (open)
        {
            openTime += Time.deltaTime * openSpeed;
            currentPos.position = new Vector3(currentPos.position.x, currentPos.position.y, Mathf.Lerp(currentPos.position.z, defaultPos.z - moveBackAmount, Time.deltaTime * openSpeed)); // move back

            if(!moveBack.isPlaying)
                moveBack.Play(); // Nessie to finish programming sounds

            if (openTime >= 1f)
            {
                currentPos.position = new Vector3(currentPos.position.x, Mathf.Lerp(currentPos.position.y, defaultPos.y + moveUpkAmount, Time.deltaTime * openSpeed), currentPos.position.z); // move up

                if(!moveUp.isPlaying)
                    moveUp.Play(); // Nessie to finish programming sounds
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (unlocked && collision.gameObject.tag == "Player")
            open = true;
    }
}
