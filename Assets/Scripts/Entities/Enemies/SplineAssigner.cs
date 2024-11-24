using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;

[System.Serializable]
public struct SplineEnemy
{
    public GameObject enemy;
    public SplineAnimate splineAnimate;
}

public class SplineAssigner : MonoBehaviour
{
    [SerializeField] private SplineEnemy[] enemies;
    [SerializeField] private SplineContainer[] splines;
    [SerializeField] private float splineInterval;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AssignSplines());

        for (int i=0; i<enemies.Length; i++)
        {
            enemies[i].enemy.SetActive(false);
        }
    }

    private IEnumerator AssignSplines()
    {
        int splinesAssigned = 0;

        while (splinesAssigned < enemies.Length)
        {
            for (int i=0; i<splines.Length; i++)
            {
                //assign first available spline to next spline animate
                if (splinesAssigned < enemies.Length)
                {
                    enemies[splinesAssigned].enemy.SetActive(true);
                    SplineAnimate target = enemies[splinesAssigned].splineAnimate;
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
