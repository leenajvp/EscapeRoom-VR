using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private int nextScene;
    private CanvasGroup canvasGroup;

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

    //private void Update()
    //{
    //    AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(nextScene);
    //    progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f); // fill progress bar

    //    if (loadingOperation.progress > 0.9f)
    //        StartCoroutine(FadeLoadingScreen(1));
    //}

    //private IEnumerator FadeLoadingScreen(float duration)
    //{
    //    float startValue = canvasGroup.alpha;
    //    float time = 0;
    //    while (time < duration)
    //    {
    //        canvasGroup.alpha = Mathf.Lerp(startValue, 1, time / duration);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    canvasGroup.alpha = 1;
    //}
}
