using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectDifficulty : MonoBehaviour
{
    // Start is called before the first frame update
    ToggleGroup toggleGroup;
    [SerializeField] GameObject easyToggle;
    [SerializeField] GameObject medToggle;
    [SerializeField] GameObject hardToggle;
    GameManager gameManager;
    Toggle easyToggleComp;
    Toggle medToggleComp;
    Toggle hardToggleComp;
    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        toggleGroup = this.GetComponent<ToggleGroup>();
        easyToggleComp = easyToggle.GetComponent<Toggle>();
        medToggleComp = medToggle.GetComponent<Toggle>();
        hardToggleComp = hardToggle.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onToggleClick(GameObject clickedOn) {
        if(medToggleComp.isOn == true) {
            gameManager.difficulty = 2;
        } else if(hardToggleComp.isOn == true) {
            gameManager.difficulty = 3;
        } else{
            gameManager.difficulty = 1;
        }
    }
}
