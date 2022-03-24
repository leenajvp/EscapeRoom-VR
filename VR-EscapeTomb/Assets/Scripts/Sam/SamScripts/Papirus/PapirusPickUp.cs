using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapirusPickUp : MonoBehaviour
{
    public Papirus Item;
    public enum PapirusNumber {Papirus1,Papirus2,Papirus3 };
    public PapirusNumber papirusNumber;
    void PickUp()
    {
        Destroy(gameObject);
        GameManager.instance.Add(Item);
        
    }
   
    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && (collision.gameObject.layer == 8 || collision.gameObject.layer ==9))
        {
           if(papirusNumber == PapirusNumber.Papirus1)
            {
                PickUp();
                QuestManager.instance.quest4.part1 = true;
            }
            if (papirusNumber == PapirusNumber.Papirus2)
            {
                PickUp();
                QuestManager.instance.quest4.part2 = true;
            }
            if (papirusNumber == PapirusNumber.Papirus3)
            {
                PickUp();
                QuestManager.instance.quest4.part3 = true;
            }

        }
    }


}
