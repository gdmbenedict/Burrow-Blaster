using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject scrap;
    [SerializeField] private Weapon weapon;
    private bool onScreen;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        //instantiate scrap object
        Instantiate(scrap, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    //Method that makes the enemy attack with its weapon
    public void Attack(Transform target)
    {
        if (weapon.GetCanFire())
        {
            weapon.ChangeProjectileDirection(target.position - weapon.GetMuzzlePos().position);
            weapon.Fire();
        }
    }
}
