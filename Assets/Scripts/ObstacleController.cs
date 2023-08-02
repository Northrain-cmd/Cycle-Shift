using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    PlayerBike player;
    ComputerBike comp;
    private void Awake() {
        player = GameObject.FindObjectOfType<PlayerBike>();
        comp = GameObject.FindObjectOfType<ComputerBike>();
    }
    // Start is called before the first frame update
   private void OnTriggerEnter2D(Collider2D other) {
    if(other.GetType() == typeof(EdgeCollider2D)) {
        if(other.tag == "Player") {
            player.HitObstacle();
        } else {
            comp.HitObstacle();
        }
        Vector3 newPos = other.gameObject.transform.position;
        newPos.x -= 50f * Time.deltaTime;
        other.gameObject.transform.position = newPos;
    }
   }
}
