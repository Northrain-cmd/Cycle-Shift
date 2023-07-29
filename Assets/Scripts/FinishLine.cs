using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public bool finishCrossed = false;
    public bool hasPlayerWon;
    private void OnTriggerEnter2D(Collider2D other) {
        if(!finishCrossed) {
            if(other.tag == "Player") {
                hasPlayerWon = true;
            } else {
                hasPlayerWon = false;
            }
            finishCrossed = true;
        }
    }
}
