using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
  [SerializeField] GameObject pauseScreen;
  bool paused = false;
    // Start is called before the first frame update
  public void onPause() {
    if(paused) {
      Time.timeScale = 1;
      paused = false;
      pauseScreen.SetActive(false);
    } else{
      Time.timeScale = 0;
      paused = true;
      pauseScreen.SetActive(true);
    }
  }

   void Update() {
    if(Input.GetKeyDown(KeyCode.Escape)) {
      onPause();
    }
  }

  public void OnContinue() {
    Time.timeScale = 1;
    paused = false;
    pauseScreen.SetActive(false);
  }
}
