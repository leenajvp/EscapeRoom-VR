using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RotationPuzzle
{
    public class TopRing : RotaionPuzzleRings
    {

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }
        protected override void CheckRingPos()
        {
            //  base.CheckRingPos();

           // Debug.Log(transform.eulerAngles.y);

            if (transform.eulerAngles.y >= LowRotationTarget && transform.eulerAngles.y <= HighRotationTarget)
            {
              //  Debug.Log("Top is Correct");
                rotaPuzzleMain.isTopPartCompleted = true;
            }

            else
            {
               // Debug.Log("Top is Wrong");
                rotaPuzzleMain.isTopPartCompleted = false;
            }
        }
    }
}
