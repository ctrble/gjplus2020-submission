using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour {
  public GameObject clearClouds;
  public GameObject rainClouds;
  public GameObject rainParticles;
  public bool isRain;
  [Range(0f, 1f)]
  public float rainChance;
  public float timeToStartRaining;
  [SerializeField]
  private float rainCountdown;

  void OnEnable() {
    isRain = Random.value <= rainChance;

    clearClouds.SetActive(!isRain);
    rainClouds.SetActive(isRain);
    rainParticles.SetActive(false);
    rainCountdown = 0f;
  }

  void Update() {
    if (isRain) {
      rainCountdown += Time.deltaTime;
      if (rainCountdown >= timeToStartRaining) {
        rainParticles.SetActive(isRain);
      }
    }
  }
}
