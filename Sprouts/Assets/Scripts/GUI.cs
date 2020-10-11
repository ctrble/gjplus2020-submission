using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// http://unity.grogansoft.com/fade-your-ui-in-and-out/
public class GUI : MonoBehaviour {
  public Canvas canvas;
  public CanvasGroup canvasGroup;
  public GameObject intro;
  public GameObject pauseScreen;
  public bool isPaused;

  void Awake() {
    canvas = GetComponent<Canvas>();
    canvasGroup = GetComponent<CanvasGroup>();

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
    float fadeTime = 1f;
    StartCoroutine(FadeCanvas(canvasGroup, 1f, 0f, fadeTime));
    Invoke("DisableIntro", fadeTime);
  }

  void DisableIntro() {
    intro.SetActive(false);
  }

  public IEnumerator FadeCanvas(CanvasGroup canvas, float startAlpha, float endAlpha, float duration) {
    // keep track of when the fading started, when it should finish, and how long it has been running
    float startTime = Time.time;
    float endTime = Time.time + duration;
    float elapsedTime = 0f;

    // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
    canvas.alpha = startAlpha;

    // loop repeatedly until the previously calculated end time
    while (Time.time <= endTime) {
      elapsedTime = Time.time - startTime; // update the elapsed time
      float percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
      if (startAlpha > endAlpha) // if we are fading out/down
      {
        canvas.alpha = startAlpha - percentage; // calculate the new alpha
      }
      else // if we are fading in/up
      {
        canvas.alpha = startAlpha + percentage; // calculate the new alpha
      }

      yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
    }
    canvas.alpha = endAlpha; // force the alpha to the end alpha before finishing – this is here to mitigate any rounding errors, e.g. leaving the alpha at 0.01 instead of 0
  }

  void OnDisable() {
    // safety first
    CancelInvoke();
    StopAllCoroutines();
  }
}
