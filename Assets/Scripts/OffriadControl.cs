using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffriadControl : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D other) {
        string bike = "Computer";
        if(other.GetType() == typeof(EdgeCollider2D) && other.gameObject.tag == "Player") {
            bike = "Player";
        }
        gameManager.BikeIsOffroad(bike);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        string bike = "Computer";
        if(other.GetType() == typeof(EdgeCollider2D) && other.gameObject.tag == "Player") {
            bike = "Player";
        }
        gameManager.BikeIsOnRoad(bike);
    }
}
