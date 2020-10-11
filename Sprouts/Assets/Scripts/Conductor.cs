using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour {
  public static Conductor instance = null;
  public AudioSource audioSource;
  public AudioClip[] tracks;
  public int currentTrack;

  void Awake() {
    CreateSingleton();
  }

  void CreateSingleton() {
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);

    DontDestroyOnLoad(gameObject);
  }

  void OnEnable() {
    audioSource = GetComponent<AudioSource>();
  }

  void Update() {
    // play the next song
    if ((!audioSource.isPlaying) || Input.GetKeyDown(KeyCode.M)) {
      currentTrack += 1;
      if (currentTrack >= tracks.Length) {
        currentTrack = 0;
      }

      audioSource.clip = tracks[currentTrack];
      audioSource.Play();
    }
  }
}
