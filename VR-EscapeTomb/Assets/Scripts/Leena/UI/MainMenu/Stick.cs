using System.Collections;
using UnityEngine;
public class Stick : MonoBehaviour
{
    public bool isLit;
    public GameObject fire;
    public float burnTime = 10.0f;

    private void Start()
    {
        fire.SetActive(false);
        isLit = false;
    }

    void Update()
    {
        if (isLit)
        {
            fire.SetActive(true);
        }

        else
            fire.SetActive(false);
    }

    public IEnumerator BurnTimer()
    {
        yield return new WaitForSeconds(burnTime);
        isLit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLit)
        {
            IDiegeticUI light = other.gameObject.GetComponent<IDiegeticUI>();

            if (light != null)
                light.GetEvent();
        }
    }

}
