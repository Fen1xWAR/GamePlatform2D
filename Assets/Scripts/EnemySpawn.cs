using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour

{
    [SerializeField] private GameObject[] Enemy;
    [SerializeField] private Transform[] SpawnPoints;
    private int SpawnRandomChance;
    private int RandomEnemy;
    public void SpawnEnemy()
    {
        for (int i = 0; i < SpawnPoints.Length; i++) {
            RandomEnemy = Random.Range(0, Enemy.Length);
            Instantiate(Enemy[RandomEnemy], SpawnPoints[i].transform.position, Quaternion.identity);
                }
        Object.Destroy(gameObject);
        //gameObject.SetActive(false);
    }

}
