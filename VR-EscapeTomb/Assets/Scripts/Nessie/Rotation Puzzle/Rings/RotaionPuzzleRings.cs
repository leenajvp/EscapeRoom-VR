using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RotationPuzzle {
    public class RotaionPuzzleRings : MonoBehaviour
    {
        [Header("Target Rotations")]
        public float LowRotationTarget;
        public float HighRotationTarget;


        protected RotationPuzzleMain rotaPuzzleMain;

        protected virtual void Start()
        {
            rotaPuzzleMain = GetComponentInParent<RotationPuzzleMain>();

            rotaPuzzleMain.isMiddlePartCompleted = false;
            rotaPuzzleMain.isTopPartCompleted = false;


        }

        // Update is called once per frame
        protected virtual void Update()
        {
            //Required to find the correct eulerAngles needed - due to the objects scaling.
            // Debug.Log(transform.eulerAngles.y); 

            CheckRingPos();
            
        }

        protected virtual void CheckRingPos()
        {
            if (transform.eulerAngles.y >= LowRotationTarget && transform.eulerAngles.y <= HighRotationTarget)
            {
              
                rotaPuzzleMain.isMiddlePartCompleted = true;
                
            }

            else
            {
                rotaPuzzleMain.isMiddlePartCompleted = false;
                
              


            }
        }
    }
}
