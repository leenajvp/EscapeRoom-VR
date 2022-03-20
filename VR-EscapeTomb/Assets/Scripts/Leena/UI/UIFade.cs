﻿using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFade : MonoBehaviour
{
    public enum InOut { FadeIn, FadeOut }
    public InOut fadeInOrOut;
    [Header("Fade In/Out Speed")]
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected CanvasGroup canvasGroup;
    protected float zeroAlpha = 0f;

    protected virtual void Start()
    {
        if (canvasGroup == null)
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

        if (canvasGroup.alpha <= 0.1)
        {
            gameObject.SetActive(false);
        }
    }
}
