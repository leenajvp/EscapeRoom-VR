﻿using UnityEngine;

public class ObjectHides : MonoBehaviour
{
    public enum MoveDir { xPlus, zPlus, xMinus, zMinus }
    public bool unlocked = false;
    private bool open = false;

    [Header("Movement Values")]
    public MoveDir moveAxis;
    [SerializeField] private float moveBackAmount = 0.1f;
    [SerializeField] private float moveUpkAmount = 0.6f;
    [SerializeField] private float openSpeed = 0.1f;
    private Vector3 defaultPos;
    private Transform currentPos;
    private float openTime = 0f;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource moveBack = null;
    [SerializeField] private AudioSource moveUp = null;

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

            if (moveAxis == MoveDir.xMinus)
                currentPos.position = new Vector3(Mathf.Lerp(currentPos.position.x, defaultPos.x - moveBackAmount, Time.deltaTime * openSpeed), currentPos.position.y, currentPos.position.z); // move back x

            if (moveAxis == MoveDir.xPlus)
                currentPos.position = new Vector3(Mathf.Lerp(currentPos.position.x, defaultPos.x + moveBackAmount, Time.deltaTime * openSpeed), currentPos.position.y, currentPos.position.z); // move back x

            if (moveAxis == MoveDir.zMinus)
                currentPos.position = new Vector3(currentPos.position.x, currentPos.position.y, Mathf.Lerp(currentPos.position.z, defaultPos.z - moveBackAmount, Time.deltaTime * openSpeed)); // move back z

            if (moveAxis == MoveDir.zPlus)
                currentPos.position = new Vector3(currentPos.position.x, currentPos.position.y, Mathf.Lerp(currentPos.position.z, defaultPos.z + moveBackAmount, Time.deltaTime * openSpeed)); // move back z

            //if (!moveBack.isPlaying)
            //    moveBack.Play(); // Nessie to finish programming sounds

            if (openTime >= 1.0f)
            {
                currentPos.position = new Vector3(currentPos.position.x, Mathf.Lerp(currentPos.position.y, defaultPos.y + moveUpkAmount, Time.deltaTime * openSpeed), currentPos.position.z); // move up

                //if (!moveUp.isPlaying)
                //    moveUp.Play(); // Nessie to finish programming sounds
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (unlocked && collision.gameObject.tag == "Player")
            open = true;
    }
}
