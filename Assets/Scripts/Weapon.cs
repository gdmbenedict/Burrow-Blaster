using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float firerate;
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private Vector3 projectileDir;

    private bool canFire = true;

    public void Fire()
    {
        if (canFire)
        {
            GameObject projectileInstance = Instantiate(projectile, muzzlePosition.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetDirection(projectileDir);

            canFire = false;
            StartCoroutine(Cooldown());
        }
    }

    //Function that puts weapon operation on a timer
    private IEnumerator Cooldown()
    {
        float timer = 0f;
        float cooldown = 1f / firerate;

        while (timer < cooldown)
        {
            yield return null;

            timer += Time.deltaTime;
        }

        //allow player to fire again
        canFire = true;
    }
}
