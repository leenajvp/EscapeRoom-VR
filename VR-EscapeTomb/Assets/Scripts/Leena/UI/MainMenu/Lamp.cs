using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IDiegeticUI
{
    [Header("Lamps")]
    public bool isLit;
    [SerializeField] protected GameObject flame;
    [SerializeField] private GameObject MenuToActivate;
    [SerializeField] private List<Lamp> otherLamps = new List<Lamp>();

    protected void Start()
    {
        isLit = false;
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
            GetMenu();
            lampCheck = 0;
        }
    }

    private void GetMenu()
    {
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
