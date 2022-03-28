using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFade : MonoBehaviour
{
    public enum InOut { FadeIn, FadeOut }
    public InOut fadeInOrOut;
    [Header("Fade In/Out Speed")]
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] private bool disableOn0;
    protected float zeroAlpha = 0f;

    protected virtual void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
    }

    protected virtual void Update()
    {
        if (fadeInOrOut == InOut.FadeIn)
            FadeIn();

        else
            FadeOut();
    }

    public void FadeIn()
    {
        fadeInOrOut = InOut.FadeIn;
        canvasGroup.alpha += Time.deltaTime * speed;
    }

    public virtual void FadeOut()
    {
        fadeInOrOut = InOut.FadeOut;
        canvasGroup.alpha -= Time.deltaTime * speed;

        if (disableOn0)
        {
            if (canvasGroup.alpha <= 0.1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
