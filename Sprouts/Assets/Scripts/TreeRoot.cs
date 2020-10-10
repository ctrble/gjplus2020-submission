using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRoot : MonoBehaviour {
  public LSystem lSystem;
  public int growthStep;

  // void OnEnable() {
  //   if (lSystem == null) {
  //     lSystem = GetComponent<LSystem>();
  //   }
  // }

  public void GetLSystem(LSystem theSystem) {
    lSystem = theSystem;
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.G)) {
      growthStep++;
      lSystem?.Generate(gameObject);
    }
  }

  public void Grow() {
    growthStep++;
  }
}
