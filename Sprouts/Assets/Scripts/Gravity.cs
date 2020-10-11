using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
  public Transform world;
  public bool detach;

  void OnEnable() {
    if (world == null) {
      world = GameObject.FindGameObjectWithTag("World").transform;
    }

    Vector3 startPosition = Random.onUnitSphere * (world.localScale.x * 0.5f);
    transform.position = startPosition;
    transform.up = startPosition - Vector3.zero;

    if (detach) {
      transform.SetParent(null);
    }
  }
}
