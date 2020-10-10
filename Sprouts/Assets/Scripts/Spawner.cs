using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
  public LSystem lSystem;
  public GameObject treePrefab;

  void Start() {
    GameObject tree = Instantiate(treePrefab);
    lSystem.tree = tree;
    lSystem.Generate();
  }
}
