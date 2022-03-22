using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    public enum Scene { MainMenu, MainLevel}
    public Scene scene;

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);

        if(scene == Scene.MainMenu)
            SceneManager.LoadScene("MainMenu");

        else
        SceneManager.LoadScene("MainLevel");
    }
}
