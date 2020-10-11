using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
  public int weatherToSpawn;
  public GameObject weather;

  void OnEnable() {
    for (int i = 0; i < weatherToSpawn; i++) {
      Instantiate(weather);
    }
  }

  public void SpawnMoreWeather() {
    Instantiate(weather);
  }
}
