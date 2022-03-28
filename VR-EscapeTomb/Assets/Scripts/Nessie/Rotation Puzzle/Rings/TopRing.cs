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
           

            if (transform.eulerAngles.y >= LowRotationTarget && transform.eulerAngles.y <= HighRotationTarget)
            {
             
                rotaPuzzleMain.isTopPartCompleted = true;
            }

            else
            {
               
                rotaPuzzleMain.isTopPartCompleted = false;
            }
        }
    }
}
