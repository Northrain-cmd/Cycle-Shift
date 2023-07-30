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
    int difficulty;

    // Start is called before the first frame update
    void Awake()
    {
        finishLine = GameObject.FindObjectOfType<FinishLine>();
        mainUI.SetActive(false);
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

        } else if(isGameStarted && !isGameOver) {
            difficulty = startGameUI.GetComponentInChildren<SelectDifficulty>().setDifficulty;
            mainUI.SetActive(true);
            startGameUI.SetActive(false);
        }
         else {
            gameOverUI.SetActive(false);
            startGameUI.SetActive(true);
        }
        if(finishLine.finishCrossed) {
            isGameOver = true;
        }
    }

    public void OnPlayAgain() {
        SceneManager.LoadScene(0);
    }

    public void OnStartGame() {
        isGameStarted = true;
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



}
