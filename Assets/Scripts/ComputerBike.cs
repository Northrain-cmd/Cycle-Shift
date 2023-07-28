using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBike : MonoBehaviour
{
     [SerializeField] float moveSpeed = 5f;
     [SerializeField] float gearStep = 2f;
     float timerMove = 0f;
     float timerShift = 0f;
     [SerializeField] float moveDelayAmount = 0.001f;
     [SerializeField] float shiftDelayAmount = 1f;
     [SerializeField] float currentGear = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timerMove += Time.deltaTime;
        timerShift += Time.deltaTime;
        if(timerMove >= moveDelayAmount) {
            this.transform.position = new Vector3(
                this.transform.position.x + ((moveSpeed +  currentGear * gearStep) * Time.deltaTime),
                this.transform.position.y,
                this.transform.position.z
                );
            timerMove = 0f;
        }
        if(currentGear < 6 && timerShift >= shiftDelayAmount) {
            currentGear += 1;
            timerShift = 0f;
            shiftDelayAmount += 1f;
        }
    }
}
