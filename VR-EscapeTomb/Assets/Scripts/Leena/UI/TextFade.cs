using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(Text))]
public class TextFade : MonoBehaviour
{
    [Header("UI Text fade")]
    [SerializeField] private float changeSpeed = 0.5f;
    private Text text;
    private CanvasGroup canvasGroup;
    private bool change = false;

    private void Start()
    {
        text = GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
    void Update()
    {
        if (!change && canvasGroup.alpha < 0.9f)
        {
            canvasGroup.alpha += changeSpeed * Time.deltaTime;

            if (canvasGroup.alpha >= 0.9f)
                change = true;
        }

        else if (change && canvasGroup.alpha > 0.1f)
        {
            canvasGroup.alpha -= changeSpeed * Time.deltaTime;

            if (canvasGroup.alpha <= 0.1f)
                change = false;
        }
    }
}
