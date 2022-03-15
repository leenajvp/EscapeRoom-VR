using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Image sprite;

    private void Start()
    {
        sprite = GetComponent<Image>();
    }

    public void Hovered()
    {
        sprite.color = Color.red;
    }
}
