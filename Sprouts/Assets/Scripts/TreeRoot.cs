﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRoot : MonoBehaviour {
  public LSystem lSystem;
  public float sunGrowTime;
  public float waterGrowTime;
  public int initialGrowth;
  public int growthStep;
  public GameObject sun;
  [SerializeField]
  private float sunDirection;
  private float sunTimer;
  public bool watering;
  private float waterTimer;

  void OnEnable() {
    if (sun == null) {
      sun = GameObject.FindGameObjectWithTag("Sun");
    }
  }

  public void PlantTree(LSystem theSystem) {
    lSystem = theSystem;
    lSystem.Generate(gameObject);

    // turn off all branches
    for (int i = 0; i < transform.childCount; i++) {
      Transform baseBranch = transform.GetChild(i);

      for (int j = 0; j < baseBranch.childCount; j++) {
        baseBranch.GetChild(j).gameObject.SetActive(j == 0);
      }

      baseBranch.gameObject.SetActive(false);
    }
  }

  void Update() {
    HandleSunGrowth();
    HandleWaterGrowth();
  }

  void HandleSunGrowth() {
    sunDirection = Vector3.Dot(transform.position, sun.transform.position);

    if (sunDirection > 0) {
      sunTimer += Time.deltaTime;

      if (sunTimer >= sunGrowTime) {
        sunTimer = 0f;
        Grow(1);
      }
    }
  }

  void HandleWaterGrowth() {
    if (watering) {
      waterTimer += Time.deltaTime;

      if (waterTimer >= waterGrowTime) {
        waterTimer = 0f;
        Grow(1);
      }
    }
  }

  public void Grow(int amount) {
    // grow the main branches first
    if (initialGrowth < transform.childCount) {
      initialGrowth += amount;

      for (int i = 0; i < transform.childCount; i++) {
        transform.GetChild(i).gameObject.SetActive(i < initialGrowth);
      }
    }
    // then grow the smaller ones
    else {
      growthStep += amount;

      foreach (Transform branchBase in transform) {
        for (int i = 0; i < branchBase.childCount; i++) {
          // checking for equality because it has a head start w/ initial growth
          branchBase.GetChild(i).gameObject.SetActive(i <= growthStep);
        }
      }
    }
  }

  void OnTriggerEnter(Collider other) {
    watering = other.CompareTag("Rain Area");
  }

  void OnTriggerExit(Collider other) {
    watering = !other.CompareTag("Rain Area");
  }
}
