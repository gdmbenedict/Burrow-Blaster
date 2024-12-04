using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineBehaviour : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;
    private bool movingForward = true;
    private float lastElapsedTimeRemainder = 0;

    // Update is called once per frame
    void Update()
    {
        //Update Logic to handle changing direction when ping-ponging

        //check if spline Animate is set to ping-ponging
        if (splineAnimate.Loop == SplineAnimate.LoopMode.PingPong)
        {
            //calculating elapsed Time remainder
            float elapsedTimeRemainder = splineAnimate.ElapsedTime % splineAnimate.Duration;

            //check if spline has been gone through
            if (elapsedTimeRemainder < lastElapsedTimeRemainder)
            {
                Debug.Log("TRying to change direction");
                if (movingForward)
                {
                    splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.NegativeZAxis;
                }
                else
                {
                    splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.ZAxis;
                }

                //switch bool tracking if enemy is moving forward
                movingForward = !movingForward;
            }

            lastElapsedTimeRemainder = elapsedTimeRemainder;
        }
    }
}
