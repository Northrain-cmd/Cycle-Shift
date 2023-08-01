using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBike : MonoBehaviour
{
     [SerializeField] int currentGear = 1;
     GameManager gameManager;
     Animator animator;
    [SerializeField] AnimationCurve[] speedPerGear;
    float boostSpeed = 0;
    [SerializeField] float boostTime = 1f;
    float [] maxSpeeds = {5, 8, 12, 18, 23, 28};
    float timePassed = 0f;
    public float curSpeed = 0f;
    public float difficultyCoefficient = 0.1f;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        animator.SetBool("isRiding", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameStarted) {
            difficultyCoefficient = gameManager.getDifficultyLevel();
            timePassed += Time.deltaTime - (Time.deltaTime * difficultyCoefficient);
            animator.SetBool("isRiding", true);
            Vector3 newPosition = this.transform.position;
                curSpeed = (speedPerGear[currentGear - 1].Evaluate(timePassed)) + boostSpeed;
               // Debug.Log(curSpeed);
                if(curSpeed >= maxSpeeds[currentGear - 1] && currentGear < 6) {
                    deboost();
                    currentGear++;
                    timePassed = 0f;
                }
                newPosition.x += curSpeed * Time.deltaTime;
                this.transform.position = newPosition;
        }
    }

    public void boost() {
        animator.SetBool("isBoosting", true);
        boostSpeed = 5f;
        Invoke(nameof(deboost), boostTime);
    }

    private void deboost() {
        animator.SetBool("isBoosting", false);
        boostSpeed = 0f;
        if(currentGear < 6) {
            timePassed -= boostTime;
        }
    }
}
