using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapirusPickUp : MonoBehaviour
{
    public Papirus Item;
    void PickUp()
    {
        Destroy(gameObject);
        GameManager.instance.Add(Item);
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PickUp();
            GameManager.instance.quest4.currentAmmount++;

        }
    }


}
