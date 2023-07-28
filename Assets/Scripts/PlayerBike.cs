using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBike : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float gearStep = 2f;
    public float currentGear = 1f;
    Slider gearSlider;
    void Start()
    {
        gearSlider = GameObject.FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow)) {
            Vector3 newPosition = this.transform.position;
            newPosition.x += (moveSpeed + gearStep * currentGear) * Time.deltaTime;
            this.transform.position = newPosition;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            Vector3 newPosition = this.transform.position;
            newPosition.x -= (moveSpeed + gearStep * currentGear) * Time.deltaTime;
            this.transform.position = newPosition;
        }
         if(Input.GetKeyDown(KeyCode.Space)) {
            if(currentGear < 6) {
                currentGear += 1;
                gearSlider.value += 1;
            }
        }
    }
}
