using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToSpawnPosition : MonoBehaviour
{
    public float timer;
    public int distanceFromPlayer;
    bool isLost = false;
    [SerializeField] Transform respownPoint;
    public Transform player;
    // Start is called before the first frame update
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
