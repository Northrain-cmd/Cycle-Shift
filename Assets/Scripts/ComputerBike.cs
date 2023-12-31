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
    bool isInLeftLane = true;
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
            checkLane();
            ChangeLane();
            difficultyCoefficient = gameManager.getDifficultyLevel()["shiftSpeed"];
            timePassed += Time.deltaTime - (Time.deltaTime * difficultyCoefficient);
            animator.SetBool("isRiding", true);
            Vector3 newPosition = this.transform.position;
                curSpeed = (speedPerGear[currentGear - 1].Evaluate(timePassed)) + boostSpeed;
               Debug.Log(curSpeed);
                if(curSpeed >= maxSpeeds[currentGear - 1] && currentGear < 6) {
                    deboost();
                    currentGear++;
                    timePassed = 0f;
                }
                newPosition.x += curSpeed * Time.deltaTime;
                this.transform.position = newPosition;
        }
        if(gameManager.isGameOver) {
            animator.SetBool("isRiding", false);
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

     public void HitObstacle() {
        if(animator.GetBool("isBoosting")) {
            deboost();
        }
        if(currentGear > 1) {
            timePassed -= Time.deltaTime;
            currentGear -= 1;
        }
    }


    private void checkLane() {
        if(transform.position.y == -7f) {
            isInLeftLane = true;
        } else if(transform.position.y == -8.5f) {
            isInLeftLane = false;
        }
    }

    private void ChangeLane() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 2f, LayerMask.GetMask("Obstacle"));
        if(hit.collider != null) {
            Vector2 targetPosition = transform.position;
            if(isInLeftLane) {
                targetPosition.y = -8.5f;
            } else {
                targetPosition.y = -7f;
            }
            StartCoroutine(Transition(targetPosition, 0.25f));
        }

    }

    IEnumerator Transition(Vector2 targetPosition, float duration) {
        float time = 0;
        Vector2 startPosition = transform.position;
            while(time < duration) {
                transform.position = Vector2.Lerp(startPosition, targetPosition, time/duration);
                time += Time.deltaTime;
                yield return null;
        }
        transform.position = targetPosition;
        timePassed -= timePassed * 0.2f;
    }
}
