﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushLight : MonoBehaviour, IDiegeticUI
{
    [SerializeField] protected GameObject flame;
    public bool isLit;
    [SerializeField] private GameObject MenuToActivate;
    [SerializeField] private List<RushLight> otherLamps = new List<RushLight>();

    protected void Start()
    {
        if (!isLit)
            flame.SetActive(false);

        MenuToActivate.SetActive(false);
    }
    public void GetEvent()
    {
        int lampCheck = 0;

        foreach (var lamp in otherLamps)
        {
            if (lamp.isLit)
                return;

            if (!lamp.isLit)
                lampCheck++;
        }

        if (lampCheck == otherLamps.Count)
        {
            flame.SetActive(true);
            isLit = true;
            StartCoroutine(BeginEventTimer());
        }
    }

    private IEnumerator BeginEventTimer()
    {
        yield return new WaitForSeconds(2);
        MenuToActivate.SetActive(true);
        MenuToActivate.GetComponent<UIFade>().FadeIn();
    }

    public void CancelEvent()
    {
        isLit = false;
        flame.SetActive(false);
        MenuToActivate.GetComponent<UIFade>().FadeOut();
    }
}
