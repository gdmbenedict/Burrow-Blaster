using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private Camera cam;
    [SerializeField] private Plane[] cameraFrustem;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        enemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraFrustem = GeometryUtility.CalculateFrustumPlanes(cam);

        for (int i=0; i<enemies.Count; i++)
        {
            HandleScreenCheck(enemies[i]);
        }
    }

    private void HandleScreenCheck(Enemy enemy)
    {
        Bounds bounds = enemy.GetComponent<Collider>().bounds;
        bool onScreen = GeometryUtility.TestPlanesAABB(cameraFrustem, bounds);

        if (onScreen && !enemy.GetOnScreen() || !onScreen && enemy.GetOnScreen())
        {
            enemy.ToggleOnScreen();
        }
    }

    public Transform GetPlayerLocation()
    {
        return player.transform;
    }

    public void AddToEnemies(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveFromEnemies(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
