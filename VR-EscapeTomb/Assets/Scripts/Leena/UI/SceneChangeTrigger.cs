using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    [Header("Load Scene")]
    [Tooltip("Menu = 0, Game Level = 1")]
    [SerializeField] private int sceneToLoad;
     private Canvas loadCanvas;
   [SerializeField] private GameObject player;

    private void Start()
    {

        PlayerPrefs.SetInt("NextScene", sceneToLoad);

       // loadCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
            SceneManager.LoadScene(2);
            //loadCanvas.gameObject.SetActive(true);
            //StartCoroutine(LoadScene());
        

    }

    //private IEnumerator LoadScene()
    //{
    //    yield return new WaitForSeconds(1);
        
    //    SceneManager.LoadScene(0);
    //}
}
