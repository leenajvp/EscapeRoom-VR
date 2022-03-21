using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToSpawnPosition : MonoBehaviour
{
    public float timer = 60f;
    public int distanceFromPlayer = 1;
    bool isLost = false;
    [SerializeField] Transform respownPoint;
    public Transform player;


    private void Start()
    {
        // Added this for those who are lazy to add inspector stuff like me - Leena
        if (respownPoint == null) { try { respownPoint = transform; } catch { Debug.LogError(name + "Spawn point is null"); } }
        if (player == null) { try { player = GameObject.Find("Player").gameObject.transform; } catch { Debug.LogError(name + "object named Player not found in hierarchy"); } }
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

        transform.position = respownPoint.transform.position;
        timer = 60;
        isLost = false;
    }

}
