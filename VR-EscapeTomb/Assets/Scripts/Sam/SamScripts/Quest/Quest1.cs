using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public GameObject spawnPoint;
    public bool snake, bowl, feather;
    public GameObject obj1;
    
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Snake") && snake)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            QuestManager.instance.RequiredAmmount++;
            obj1.SetActive(true);
        }
        else if (other.gameObject.tag == "Bowl" && bowl)

        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            QuestManager.instance.RequiredAmmount++;
            obj1.SetActive(true);
        }
        else if (other.gameObject.tag == "Feather" && feather)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            QuestManager.instance.RequiredAmmount++;
            obj1.SetActive(true);
        }
    }
    
    
}
