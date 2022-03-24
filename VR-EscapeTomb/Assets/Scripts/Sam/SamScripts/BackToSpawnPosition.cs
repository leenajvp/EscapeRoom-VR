using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToSpawnPosition : MonoBehaviour
{

    public float timer = 60f;
    public int distanceFromPlayer = 1;
    bool isLost = false;
    [SerializeField] Vector3 respownPoint;
    public Transform player;

    private Quaternion rotation; // want the rotation to be original too - leena
    private float setTimer; // So the timer does not reset to 60, but instead  to the value set on inspector before starting - Leena

    private void Start()
    {
        // Added this for those who are lazy to add inspector stuff like me - Leena
        if (respownPoint == new Vector3( 0,0,0)) { try { respownPoint = gameObject.transform.position; } catch { Debug.LogError(name + "Spawn point is null"); } } // The object does not need a separate spawn point, it will automatically spawn to the orifinal location unless the spwan point is changed from 0,0,0 - Leena
        if (player == null) { try { player = GameObject.Find("Player").gameObject.transform; } catch { Debug.LogError(name + "object named Player not found in hierarchy"); } }

        rotation = gameObject.transform.rotation;
        setTimer = timer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 12)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance > distanceFromPlayer)
            {
                isLost = true;
            }
            
        }
    }
    public void Update()
    {
        if(isLost)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                Reset();
            }
        }
        
    }
    private void Reset()
    {
        transform.rotation = rotation;
        transform.position = respownPoint;
        timer = setTimer; // sets to the time set in inspector - Leena
        isLost = false;
    }

}
