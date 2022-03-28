using UnityEngine;
using UnityEngine.UI;

public class UpdateText : UIFade
{
    [Header("Text to update")]
    [SerializeField] private Text text = null;
    [SerializeField] private string newText = "";
    [SerializeField] private bool changed;

    protected override void Update()
    {
        base.Update();

        if (changed)
        {
            if (canvasGroup.alpha <= 0.1)
            {
                text.text = newText;
                FadeIn();
            }
        }
    }

    public void SetText()
    {
        FadeOut();
        changed = true;
    }

    public override void FadeOut()
    {
        fadeInOrOut = InOut.FadeOut;
        canvasGroup.alpha -= Time.deltaTime * speed;
    }
}
