using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyType> enemyTypes;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnInterval = 1f;

    List<SplineContainer> splines;
    EnemyFactory enemyFactory;

    float spawnTimer;
    int enemiesSpawned;
}
