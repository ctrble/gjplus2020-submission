using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour {
  public GameObject intro;
  public GameObject pauseScreen;
  public bool isPaused;

  void Awake() {
    intro.SetActive(true);
    pauseScreen.SetActive(false);

    Invoke("HideIntro", 3f);
  }

  void Update() {
    // listen for pause game
    if (Input.GetKeyDown(KeyCode.Escape)) {
      pauseScreen.SetActive(true);
      isPaused = true;
      Time.timeScale = 0f;
    }
  }

  public void ReturnToGame() {
    pauseScreen.SetActive(false);
    isPaused = false;
    Time.timeScale = 1f;
  }

  public void ResetGame() {
    SceneManager.LoadScene(0, LoadSceneMode.Single);
    Time.timeScale = 1f;
  }

  void HideIntro() {
    intro.SetActive(false);
  }
}
