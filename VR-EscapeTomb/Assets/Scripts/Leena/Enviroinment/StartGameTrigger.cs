using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameTrigger : MonoBehaviour
{
    bool enter;
    private void Update()
    {
        if (enter)
            Debug.Log("enter");
    }

    private void OnTriggerStay(Collider other)
    {
        enter = true;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("MainLevel");
    }
}
