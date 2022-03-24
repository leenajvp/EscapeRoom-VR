using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameTime : MonoBehaviour
{
    private static bool startGame = false; // What is this used for?

    public Text timerText;
    public float totalTime;
    public bool hasTime;
    

    public float startTime = 900;

    [Header("Game Over")]
    [SerializeField] private GameObject gameoverMenu;
    [SerializeField] private Text gameoverTimerTxt;

    // Start is called before the first frame update
    void Start()
    {

        // startGame = true;

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
        //  if (startGame == true) // not needed
        // {
        if (hasTime)
        {
            if (startTime > 0)
            {
                startTime -= Time.deltaTime;
                timerText.text = startTime.ToString();
                float minutes = Mathf.FloorToInt(startTime / 60);
                float seconds = Mathf.FloorToInt(startTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            else
            {
                gameoverMenu.SetActive(true);
                StartCoroutine(EndGame());
            }

            // ORIGINAL just comment this back in if preferred

            // timerText.text = string.Format(" {0} : {1} ", minutes.ToString("00"), seconds.ToString("00"));
            //    Debug.Log(timerText.text);


            //totalTime -= Time.deltaTime;
            //    timerText.text = totalTime.ToString();
            //    int minutes = (int)totalTime / 60;
            //    int seconds = (int)totalTime % 60;
            //    timerText.text = string.Format(" {0} : {1} ", minutes.ToString("00"), seconds.ToString("00"));
            //    Debug.Log(timerText.text);
        }

        else
        {
            totalTime += Time.deltaTime;
            timerText.text = totalTime.ToString();
            int minutes = (int)totalTime / 60;
            int seconds = (int)totalTime % 60;
            timerText.text = string.Format(" {0} : {1} ", minutes.ToString("00"), seconds.ToString("00"));
            // Debug.Log(timerText.text);
        }
        //  }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(totalTime / 60);
        float seconds = Mathf.FloorToInt(totalTime %60);
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
