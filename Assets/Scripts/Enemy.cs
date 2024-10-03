using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject scrap;
    [SerializeField] private Weapon weapon;
    private bool onScreen;

    // Start is called before the first frame update
    void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.AddToEnemies(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (onScreen)
        {
            Attack(enemyManager.GetPlayerLocation());
        }
    }

    public void ToggleOnScreen()
    {
        onScreen = !onScreen;
        if (onScreen)
        {
            //added in to give player moment before the enemy starts shooting
            weapon.ActivateCooldown();
        }
    }

    public bool GetOnScreen()
    {
        return onScreen;
    }

    public void Die()
    {
        //instantiate scrap object
        Instantiate(scrap, transform.position, Quaternion.identity);
        enemyManager.RemoveFromEnemies(this);
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
