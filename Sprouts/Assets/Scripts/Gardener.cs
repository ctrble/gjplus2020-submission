using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gardener : MonoBehaviour {
  public LSystem lSystem;
  public Vector3[] seeds;
  public GameObject treePrefab;

  void Start() {
    for (int i = 0; i < seeds.Length; i++) {
      if (i > 0) {
        return;
      }
      GameObject tree = Instantiate(treePrefab);

      lSystem.Generate(tree);
      tree.transform.position = seeds[i];
    }
  }
}
