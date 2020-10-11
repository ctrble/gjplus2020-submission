using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRoot : MonoBehaviour {
  public LSystem lSystem;
  public int growthStep;

  public void PlantTree(LSystem theSystem) {
    lSystem = theSystem;
    lSystem.Generate(gameObject);

    foreach (Transform branchBase in transform) {
      for (int i = 0; i < branchBase.childCount; i++) {
        branchBase.GetChild(i).gameObject.SetActive(i <= growthStep);
      }
    }
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.G)) {
      Grow(1);
    }
  }

  public void Grow(int amount) {
    growthStep += amount;

    foreach (Transform branchBase in transform) {
      for (int i = 0; i < branchBase.childCount; i++) {
        branchBase.GetChild(i).gameObject.SetActive(i <= growthStep);
      }
    }
  }
}
