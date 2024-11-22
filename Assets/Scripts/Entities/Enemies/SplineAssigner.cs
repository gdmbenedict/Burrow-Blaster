using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineAssigner : MonoBehaviour
{
    [SerializeField] private SplineAnimate[] splineAnimates;
    [SerializeField] private SplineContainer[] splines;
    [SerializeField] private float splineInterval;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AssignSplines());
    }

    private IEnumerator AssignSplines()
    {
        int splinesAssigned = 0;

        while (splinesAssigned < splineAnimates.Length)
        {
            for (int i=0; i<splines.Length; i++)
            {
                //assign first available spline to next spline animate
                if (splinesAssigned < splineAnimates.Length)
                {
                    SplineAnimate target = splineAnimates[splinesAssigned];
                    target.Container = splines[i];
                    target.Play();
                    splinesAssigned++;
                }
                else
                {
                    break;
                }

                //wait for interval to assign new splines
                yield return new WaitForSeconds(splineInterval);
            }
        }
    }

}
