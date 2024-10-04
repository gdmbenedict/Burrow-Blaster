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

    //Function that fires a projectile
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

    //Function that allows for changing of direction in which a projectile is fired
    public void ChangeProjectileDirection(Vector3 direction)
    {
        projectileDir = direction;
    }

    public Transform GetMuzzlePos()
    {
        return muzzlePosition;
    }

    public bool GetCanFire()
    {
        return canFire;
    }

    public void ActivateCooldown()
    {
        canFire = false;
        StartCoroutine(Cooldown());
    }

    public void Disable()
    {
        StopCoroutine(Cooldown());
        canFire = false;
    }

    public void Enable()
    {
        canFire = true;
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
