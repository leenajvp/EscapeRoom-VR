using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public GameObject spawnPoint;
    public bool snake, bowl, feather;
    public string questToCheck;
    void Update()
    {
        if(GameManager.instance.RequiredAmmount == 3)
        {
            QuestManager.instance.MarkQuestIfComplete(questToCheck);
        }
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Snake") && snake)
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Rigidbody>().transform.position = spawnPoint.transform.position;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            other.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            GameManager.instance.RequiredAmmount++;
        }
        else if (other.gameObject.tag == "Bowl" && bowl)

        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Rigidbody>().transform.position = spawnPoint.transform.position;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            other.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            GameManager.instance.RequiredAmmount++;
        }
        else if (other.gameObject.tag == "Feather" && feather)
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            other.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            GameManager.instance.RequiredAmmount++;
        }
    }
    

}
