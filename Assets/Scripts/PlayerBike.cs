using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBike : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AnimationCurve[] speedPerGear;
    [SerializeField] GameObject shiftNoticeTextObject;
    GameManager gameManager;
    TextMeshProUGUI shiftNoticeText;
    float [] maxSpeeds = {5, 8, 12, 18, 23, 28};
    float timePassed = 0f;
    public int currentGear = 1;
    Slider gearSlider;
    public bool isShiftAllowed = false;
    public float curSpeed = 0f;
    bool isGameOver;
    public bool isBikeOnRoad = true;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        shiftNoticeText = shiftNoticeTextObject.GetComponent<TextMeshProUGUI>();
        animator.SetBool("isRiding", false);
    }

    private void updateSlider(int value) {
        if(GameObject.FindObjectOfType(typeof(Slider)) != null) {
            gearSlider = GameObject.FindObjectOfType<Slider>();
            gearSlider.value = value - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameStarted) {
            isGameOver = gameManager.isGameOver;
            if(currentGear == 6) {
                shiftNoticeText.text = "Maximum gear reached";
            }
            if(Input.GetKey(KeyCode.RightArrow)) {
                animator.SetBool("isRiding", true);
                timePassed += Time.deltaTime;
                Vector3 newPosition = this.transform.position;
                curSpeed = (speedPerGear[currentGear - 1].Evaluate(timePassed));
                if(curSpeed == maxSpeeds[currentGear - 1]) {
                    isShiftAllowed = true;
                    if(!isGameOver && currentGear < 6) {
                        shiftNoticeText.text = "Press Space!";
                        shiftNoticeTextObject.GetComponentInParent<Image>().color = new Color(0.047f,0.96f,0,1);
                    }

                }
                Debug.Log(curSpeed);
                newPosition.x += curSpeed * Time.deltaTime;
                this.transform.position = newPosition;
            }
            if(Input.GetKey(KeyCode.LeftArrow) || (Input.GetKeyUp(KeyCode.RightArrow))) {
                timePassed = 0f;
                currentGear = 1;
                updateSlider(currentGear);
                animator.SetBool("isRiding", false);
            }
             if(Input.GetKey(KeyCode.UpArrow)){
                Vector3 newPosition = this.transform.position;
                newPosition.y += 5f * Time.deltaTime;
                transform.position = newPosition;
            }
            if(Input.GetKey(KeyCode.DownArrow)){
                Vector3 newPosition = this.transform.position;
                newPosition.y -= 5f * Time.deltaTime;
                transform.position = newPosition;
            }
            if(Input.GetKeyDown(KeyCode.Space) && isShiftAllowed && isBikeOnRoad) {
                if(currentGear < 6) {
                    currentGear += 1;
                    timePassed = 0f;
                    isShiftAllowed = false;
                    updateSlider(currentGear);
                    if(! isGameOver && currentGear < 6) {
                        shiftNoticeText.text = "Gain speed to unlock next gear";
                        shiftNoticeTextObject.GetComponentInParent<Image>().color = new Color(0.96f,0.63f,0,1);
                    }
                }
            }
        }
    }

    public void ReduceGear() {
        if(currentGear > 1) {
          currentGear -= 1;
          updateSlider(currentGear);
        }
    }
}
