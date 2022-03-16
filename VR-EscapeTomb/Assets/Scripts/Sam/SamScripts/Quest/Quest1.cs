using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public GameObject spawnPoint;
    public Goal goal;
    int RequiredAmmount;
    void Update()
    {
        if (RequiredAmmount == 3)
        {
            QuestManager.instance.MarkQuestIfComplete("Puzzle1");
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag ("Snake") && goal.snake)
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Rigidbody>().transform.position = spawnPoint.transform.position;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            other.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            RequiredAmmount++;


        }
        else if (other.gameObject.tag == "Bowl" && goal.bowl)

        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Rigidbody>().transform.position = spawnPoint.transform.position;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            other.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            RequiredAmmount++;
            
        }
        else if (other.gameObject.tag == "Feather" && goal.feather)
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            other.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            RequiredAmmount++;
            
        }
    }

}
