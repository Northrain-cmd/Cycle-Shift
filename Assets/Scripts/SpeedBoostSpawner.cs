using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostSpawner : MonoBehaviour
{
    public GameObject speedBoostPrefab;
    public float spawnRate = 1f;
    public float spawnInterval = 1f;

    public void spawnBoosts() {
        for(int i = 0; i < 10; i++) {
            SpawnSpeedBoost();
        }
    }

    void SpawnSpeedBoost() {
        // Create a new speed boost object and instantiate it at a random position.
        int rnadomNumber = Random.Range(0,2);
        float[] lanes = new float[] {-7.13f, -8.57f};
        GameObject speedBoost = Instantiate(speedBoostPrefab,
            new Vector2(Random.Range(-12f, 700f), lanes[rnadomNumber]),
           Quaternion.identity);
    }
}
