using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest4 : MonoBehaviour
{
    public int requiredAmmount;
    public int currentAmmount;
    // Start is called before the first frame update
    void Start()
    {
        requiredAmmount = 0;
        currentAmmount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmmount == requiredAmmount)
        {
            Quest4Complete();
        }
    }
    public void Quest4Complete()
    {

    }
}
