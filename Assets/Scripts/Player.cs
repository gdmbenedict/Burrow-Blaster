using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private Weapon weapon;
    [SerializeField] private List<Collider> colliders;
    // Start is called before the first frame update
    void Start()
    {
        model.SetActive(true);
        for (int i = 0; i < colliders.Count; i++)
        {
            colliders[i].enabled = true;
        }
        weapon.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        model.SetActive(false);
        for (int i=0; i < colliders.Count; i++)
        {
            colliders[i].enabled = false;
        }
        weapon.Disable();
    }
    
    public void DisablePlayerVisual()
    {
        model.SetActive(false);
    }
}
