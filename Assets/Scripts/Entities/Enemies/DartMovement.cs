using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMovement : MonoBehaviour
{
    [SerializeField] private float dartRange;
    [SerializeField] private float coolDownTime;
    [SerializeField] private float dartTime;
    private Vector3 center;
    private bool canCallDart = true;

    private bool onScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetPositionDelayed());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canCallDart)
        {
            StartCoroutine(DartToPosition(Random.Range(-dartRange, dartRange), Random.Range(-dartRange, dartRange)));
        }
    }

    //Function that moves dart quickly
    private IEnumerator DartToPosition(float xPos, float zPos)
    {
        //make it so dart behaviour can't be called 
        canCallDart = false;

        //set target location
        xPos += center.x;
        zPos += center.z;
        Debug.Log(zPos);
        Vector3 targetPos = new Vector3(xPos, center.y, zPos);
        Vector3 startPos = transform.position;
        //Debug.Log("start Pos: " + startPos);
        //Debug.Log("target Pos: " + targetPos);

        //move towards location
        float timer = 0;
        while (timer < dartTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timer/dartTime);
            yield return null;
            timer += Time.deltaTime;
        }

        //snap position
        transform.position = targetPos;

        //start cooldown
        StartCoroutine(DartCooldown());
    }

    //Function that manages the cooldown on movement
    private IEnumerator DartCooldown()
    {
        //wait then allow call to behaviour
        yield return new WaitForSeconds(coolDownTime);
        canCallDart = true;
    }

    private IEnumerator GetPositionDelayed()
    {
        yield return null;
        center = transform.position;
        Debug.Log(center);
    }
}
