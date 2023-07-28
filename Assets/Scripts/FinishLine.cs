using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public bool finishCrossed = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if(!finishCrossed) {
            if(other.tag == "Player") {
                Debug.Log("Player won!");
            } else {
                Debug.Log("Computer won");
            }
            finishCrossed = true;
        }
    }
}
