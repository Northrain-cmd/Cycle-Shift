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
    SpeedBoostSpawner speedBoostSpawner;
    PlayerBike playerBike;
    Animator trafficLightAnimator;
    [SerializeField] GameObject countdownTimer;
    Animator countdownTimerAnimator;
    int difficulty;

    // Start is called before the first frame update
    void Awake()
    {
        finishLine = GameObject.FindObjectOfType<FinishLine>();
        mainUI.SetActive(false);
        countdownTimer.SetActive(true);
        countdownTimerAnimator = countdownTimer.GetComponent<Animator>();
        countdownTimer.SetActive(false);
        trafficLightAnimator = trafficLight.GetComponent<Animator>();
        playerBike = GameObject.FindObjectOfType<PlayerBike>();
        speedBoostSpawner = GameObject.FindObjectOfType<SpeedBoostSpawner>();
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
        startCountdown();
        speedBoostSpawner.spawnBoosts();
    }

    public void startCountdown() {
        difficulty = startGameUI.GetComponentInChildren<SelectDifficulty>().setDifficulty;
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

    public float getDifficultyLevel() {
        if(difficulty == 1) {
            return 0.1f;
        } else if(difficulty == 2) {
            return 0.08f;
        } else {
            return 0.06f;
        }
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



}
