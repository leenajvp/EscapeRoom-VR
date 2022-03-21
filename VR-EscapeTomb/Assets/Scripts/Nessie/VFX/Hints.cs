using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    [Header("Hint Bool")]
    public bool playerNeedsHint;

    [Header("Materials")]
    public Material normalMaterial;
    public Material hintMaterial;
    private MeshRenderer thisRenderer;

    private void Start()
    {
        thisRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(playerNeedsHint)
        {
            thisRenderer.material = hintMaterial;
        }

        else
        {
            thisRenderer.material = normalMaterial;
        }
    }
}
