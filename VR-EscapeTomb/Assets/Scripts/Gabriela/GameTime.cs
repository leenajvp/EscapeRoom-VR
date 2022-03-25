using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameTime : MonoBehaviour
{
    public Text timerText;
    public float totalTime;
    public bool hasTime;
    
    public float startTime = 900;

    [Header("Game Over")]
    [SerializeField] private GameObject gameoverMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameoverMenu.SetActive(false);

        if (PlayerPrefs.GetInt("Timer") == 1)
            hasTime = true;

        else
            hasTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (hasTime)
        {
            if (startTime > 0)
            {
                startTime -= Time.deltaTime;
                timerText.text = totalTime.ToString();
                int minutes = (int)totalTime / 60;
                int seconds = (int)totalTime % 60;
                timerText.text = string.Format(" {0} : {1} ", minutes.ToString("00"), seconds.ToString("00"));
            }

            else
            {
                gameoverMenu.SetActive(true);
                StartCoroutine(EndGame());
            }
        }

        else
        {
            totalTime += Time.deltaTime;
            timerText.text = totalTime.ToString();
            int minutes = (int)totalTime / 60;
            int seconds = (int)totalTime % 60;
            timerText.text = string.Format(" {0} : {1} ", minutes.ToString("00"), seconds.ToString("00"));
        }
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
