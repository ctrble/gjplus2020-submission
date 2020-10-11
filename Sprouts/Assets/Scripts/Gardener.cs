using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gardener : MonoBehaviour {
  public LSystem lSystem;
  public int numberOfSeeds;
  public Vector3[] seeds;
  public Transform world;
  public GameObject treePrefab;

  void Start() {
    seeds = new Vector3[numberOfSeeds];

    for (int i = 0; i < seeds.Length; i++) {
      seeds[i] = Random.onUnitSphere * (world.localScale.x * 0.5f);

      TreeRoot tree = Instantiate(treePrefab).GetComponent<TreeRoot>();

      tree.PlantTree(lSystem);
      tree.transform.SetParent(world);
      tree.transform.position = seeds[i];
      tree.transform.up = seeds[i] - Vector3.zero;
    }
  }
}
