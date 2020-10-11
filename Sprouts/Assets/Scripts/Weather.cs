using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour {
  public GameObject clearClouds;
  public ParticleSystem clearParticleSystem;
  public GameObject rainClouds;
  public ParticleSystem rainParticleSystem;
  public GameObject rainParticles;
  public bool isRain;
  [Range(0f, 1f)]
  public float rainChance;
  public float timeToStartRaining;
  [SerializeField]
  private float rainCountdown;


  public World world;
  public float rotationSpeed;
  public float orbitSpeed;
  public float desiredDistance;

  void OnEnable() {
    isRain = Random.value <= rainChance;

    // set up weather effects
    clearClouds.SetActive(!isRain);
    rainClouds.SetActive(isRain);
    rainParticles.SetActive(false);
    rainCountdown = 0f;

    if (world == null) {
      world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
    }

    if (clearParticleSystem == null) {
      clearParticleSystem = clearClouds.GetComponent<ParticleSystem>();
    }

    if (rainParticleSystem == null) {
      rainParticleSystem = rainClouds.GetComponent<ParticleSystem>();
    }

    // set up fresh cloud settings
    float lifetime = Random.Range(30f, 120f);
    if (clearParticleSystem != null) {
      ParticleSystem.MainModule main = clearParticleSystem.main;
      main.duration = lifetime;
      main.startLifetime = lifetime;
      clearParticleSystem.Play();
    }

    if (rainParticleSystem != null) {
      ParticleSystem.MainModule main = rainParticleSystem.main;
      main.duration = lifetime;
      main.startLifetime = lifetime;
      rainParticleSystem.Play();
    }
  }

  void Update() {
    MoveAroundWorld();
    EnableRain();
    DisableClouds();
  }

  void MoveAroundWorld() {
    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    transform.RotateAround(world.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);

    // ensure distance is correct
    float currentDistance = Vector3.Distance(world.transform.position, transform.position);
    Vector3 towardsTarget = transform.position - world.transform.position;
    transform.position += (desiredDistance - currentDistance) * towardsTarget.normalized;
  }

  void EnableRain() {
    if (isRain) {
      // wait a bit for the clouds to form first
      rainCountdown += Time.deltaTime;
      if (rainCountdown >= timeToStartRaining) {
        rainParticles.SetActive(isRain);
      }
    }
  }

  void DisableClouds() {
    if (isRain && !rainParticleSystem.IsAlive()) {
      world.SpawnMoreWeather();
      Destroy(gameObject);
    }
    else if (!isRain && !clearParticleSystem.IsAlive()) {
      world.SpawnMoreWeather();
      Destroy(gameObject);
    }
  }
}
