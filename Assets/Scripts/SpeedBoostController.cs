using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostController : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameOver) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetType() == typeof(EdgeCollider2D)) {
            if(other.tag == "Player") {
            gameManager.boost("Player");
            audioSource.Play();
            } else {
            gameManager.boost("Computer");
        }
        }
    }
}
