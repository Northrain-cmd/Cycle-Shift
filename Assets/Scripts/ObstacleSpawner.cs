using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    GameManager gameManager;
    private float spawnNumber;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Start() {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnObstacles() {
        spawnNumber = gameManager.getDifficultyLevel()["obstaclesSpawnNumber"];
        List<float> generatedXCoords = new List<float>();
        for(int i = 0; i < spawnNumber; i++) {
            generatedXCoords.Add(SpawnObstacle(generatedXCoords));
        }
    }

    float SpawnObstacle(List<float> xCoordsArray) {
        float step = 8f;
        float xRandom = Random.Range(0f, 700f);
        float numSteps = Mathf.Floor(xRandom / step);
        float adjustedXRandom = numSteps * step;
        while(xCoordsArray.Contains(adjustedXRandom)) {
            xRandom = Random.Range(0, 700f);
            numSteps = Mathf.Floor(xRandom / step);
            adjustedXRandom = numSteps * step;
        }
        int randomNumber = Random.Range(0,2);
        float[] lanes = new float[] {-7f, -8.5f};
        GameObject obstacle = Instantiate(obstaclePrefab,
            new Vector2(adjustedXRandom, lanes[randomNumber]),
           Quaternion.identity);
        return adjustedXRandom;
    }
}
