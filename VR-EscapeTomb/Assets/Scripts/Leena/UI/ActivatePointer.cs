using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePointer : MonoBehaviour
{
    [SerializeField]private UIRaycast uiPointer;

    private void OnEnable()
    {
        uiPointer.gameObject.SetActive(true);
    }

    public void DisableRay()
    {
        uiPointer.gameObject.SetActive(false);
    }
}
