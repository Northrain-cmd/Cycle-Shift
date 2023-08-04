using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public bool isGameStarted = false;
    FinishLine finishLine;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject startGameUI;
    [SerializeField] GameObject winLoseMessage;
    [SerializeField] GameObject trafficLight;
    AudioSource trafficLightSource;
    SpeedBoostSpawner speedBoostSpawner;
    ObstacleSpawner obstacleSpawner;
    PlayerBike playerBike;
    ComputerBike computerBike;
    Animator trafficLightAnimator;
    [SerializeField] GameObject countdownTimer;
    Animator countdownTimerAnimator;
    public int difficulty;
    public Dictionary<string, float> difficultySettings;


    // Start is called before the first frame update
    void Awake()
    {
        difficulty = 1;
        difficultySettings = new Dictionary<string, float>();
        difficultySettings.Add("shiftSpeed", 0.1f);
        difficultySettings.Add("obstaclesSpawnNumber", 10f);
        trafficLightSource = trafficLight.GetComponentInParent<AudioSource>();
        finishLine = GameObject.FindObjectOfType<FinishLine>();
        mainUI.SetActive(false);
        countdownTimer.SetActive(true);
        countdownTimerAnimator = countdownTimer.GetComponent<Animator>();
        countdownTimer.SetActive(false);
        trafficLightAnimator = trafficLight.GetComponent<Animator>();
        playerBike = GameObject.FindObjectOfType<PlayerBike>();
        computerBike = GameObject.FindObjectOfType<ComputerBike>();
        speedBoostSpawner = GameObject.FindObjectOfType<SpeedBoostSpawner>();
        obstacleSpawner = GameObject.FindObjectOfType<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver) {
            mainUI.SetActive(false);
            gameOverUI.SetActive(true);
            TextMeshProUGUI winLoseMessageText = winLoseMessage.GetComponentInChildren<TextMeshProUGUI>();
            if(finishLine.hasPlayerWon) {
                winLoseMessageText.text = "You won!";
            } else {
                winLoseMessageText.text = "You lost!";

            }

        }
        if(finishLine.finishCrossed) {
            isGameOver = true;
            isGameStarted = false;
        }
    }

    public void OnPlayAgain() {
        SceneManager.LoadScene(0);
    }

    public void OnStartGame() {
        difficultySettings = getDifficultyLevel();
        startCountdown();
        speedBoostSpawner.spawnBoosts();
        obstacleSpawner.spawnObstacles();
    }

    public void startCountdown() {
        trafficLightSource.Play();
        mainUI.SetActive(true);
        startGameUI.SetActive(false);
        countdownTimer.SetActive(true);
        trafficLightAnimator.SetBool("startCountdown", true);
        Invoke("disableCountodwn", 3.05f);
    }

    private void disableCountodwn(){
        countdownTimer.SetActive(false);
        isGameStarted = true;
        trafficLightAnimator.SetBool("startCountdown", false);
    }

    public Dictionary<string, float> getDifficultyLevel() {
        if(difficulty == 1) {
            difficultySettings["shiftSpeed"] = 0.35f;
            difficultySettings["obstaclesSpawnNumber"] = 5f;
        } else if(difficulty == 2) {
            difficultySettings["shiftSpeed"] = 0.25f;
            difficultySettings["obstaclesSpawnNumber"] = 10f;
        } else {
            difficultySettings["shiftSpeed"] = 0.15f;
            difficultySettings["obstaclesSpawnNumber"] = 20f;
        }
        return difficultySettings;
    }

    public void BikeIsOffroad(string bike) {
        if(bike == "Player") {
            playerBike.ReduceGear();
            playerBike.isBikeOnRoad = false;
        }
    }

    public void BikeIsOnRoad(string bike) {
        if(bike == "Player") {
            playerBike.isBikeOnRoad = true;
        }
    }

    public void boost(string bike) {
        if(bike == "Player") {
            playerBike.boost();
        } else {
            computerBike.boost();
        }
    }

}
