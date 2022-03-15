using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFade : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected float zeroAlpha = 0f;
    [SerializeField] private float speed = 1f;

    public enum InOut {FadeIn,FadeOut }
    public InOut fadeInOrOut;

    protected virtual void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = zeroAlpha;
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
    }
}
