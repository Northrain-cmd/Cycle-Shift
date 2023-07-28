using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public bool isGameStarted = false;
    FinishLine finishLine;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject startGameUI;

    // Start is called before the first frame update
    void Start()
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
        } else if(isGameStarted && !isGameOver) {
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



}
