using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle), typeof(Text))]
public class ToggleText : MonoBehaviour
{
    private Text text => GetComponent<Text>();
    private Toggle toggle => GetComponent<Toggle>();

    public void ChangeText()
    {
        if (toggle.isOn)
            text.text = "ON";
        else
            text.text = "OFF";
    }
}
