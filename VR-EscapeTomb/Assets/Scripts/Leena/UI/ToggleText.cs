using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle), typeof(Text))]
public class ToggleText : MonoBehaviour
{
    private Text text;
    private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        text = GetComponent<Text>();
    }

    public void ChangeText()
    {
        if (toggle.isOn)
            text.text = "ON";
        else
            text.text = "OFF";
    }
}
