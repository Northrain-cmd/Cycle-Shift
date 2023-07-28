using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBike : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float gearStep = 2f;
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
    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gearSlider = GameObject.FindObjectOfType<Slider>();
        shiftNoticeText = shiftNoticeTextObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameStarted) {
            isGameOver = gameManager.isGameOver;
            if(Input.GetKey(KeyCode.RightArrow)) {
                timePassed += Time.deltaTime;
                Vector3 newPosition = this.transform.position;
                curSpeed = (speedPerGear[currentGear - 1].Evaluate(timePassed));
                if(curSpeed == maxSpeeds[currentGear - 1]) {
                    isShiftAllowed = true;
                    if(!isGameOver) {
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
            }
            if(Input.GetKeyDown(KeyCode.Space) && isShiftAllowed) {
                if(currentGear < 6) {
                    currentGear += 1;
                    gearSlider.value += 1;
                    timePassed = 0f;
                    isShiftAllowed = false;
                    if(! isGameOver) {
                        shiftNoticeText.text = "Gain speed to unlock next gear";
                        shiftNoticeTextObject.GetComponentInParent<Image>().color = new Color(0.96f,0.63f,0,1);
                    }
                }
            }
        }
    }
}
