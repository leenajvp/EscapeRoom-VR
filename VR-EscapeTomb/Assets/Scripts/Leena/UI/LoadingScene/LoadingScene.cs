using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScene : MonoBehaviour
{
    [SerializeField] private int nextScene;

    void Start()
    {
        StartCoroutine(LoadSyncOp());
    }

    private IEnumerator LoadSyncOp()
    {
        Debug.Log("Nect scene " + PlayerPrefs.GetInt("NextScene"));
        nextScene = PlayerPrefs.GetInt("NextScene");
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(nextScene);
        yield return new WaitForEndOfFrame();
    }
}
