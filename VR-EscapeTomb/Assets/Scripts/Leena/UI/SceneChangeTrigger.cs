using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    [Header("Load Scene")]
    [Tooltip("Menu = 0, Game Level = 1")]
    [SerializeField] private int sceneToLoad;
    [SerializeField] private GameObject player;

    private void Start()
    {
        PlayerPrefs.SetInt("NextScene", sceneToLoad);
    }

    private void OnTriggerStay(Collider other)
    {
        SceneManager.LoadScene(2);
    }
}
