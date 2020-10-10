using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
  public LSystem lSystem;
  public Vector3[] seeds;
  public GameObject treePrefab;

  void Start() {
    for (int i = 0; i < seeds.Length; i++) {
      GameObject tree = Instantiate(treePrefab);

      lSystem.Generate(tree);
      tree.transform.position = seeds[i];
    }
  }
}
