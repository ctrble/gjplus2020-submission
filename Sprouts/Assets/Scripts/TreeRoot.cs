using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRoot : MonoBehaviour {
  public LSystem lSystem;
  public int growthStep;
  void Update() {
    if (Input.GetKeyDown(KeyCode.G)) {
      growthStep++;
      lSystem?.Generate(gameObject);
    }
  }

  public void GetSeeded(LSystem system) {
    // TODO: honestly this is a horrible way of handling this
    growthStep++;
    lSystem = system;
  }
}
