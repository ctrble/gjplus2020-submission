using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragRotate : MonoBehaviour {

  private Vector3 mousePreviousPosition = Vector3.zero;
  private Vector3 mousePositionDelta = Vector3.zero;
  private Camera cameraMain;

  void Start() {
    cameraMain = Camera.main;
    transform.up = cameraMain.transform.up;
    transform.right = cameraMain.transform.right;
  }

  void Update() {
    if (Input.GetMouseButton(0)) {
      mousePositionDelta = Input.mousePosition - mousePreviousPosition;

      transform.Rotate(cameraMain.transform.up, -Vector3.Dot(mousePositionDelta, cameraMain.transform.right), Space.World);
      transform.Rotate(cameraMain.transform.right, Vector3.Dot(mousePositionDelta, cameraMain.transform.up), Space.World);
    }

    mousePreviousPosition = Input.mousePosition;
  }
}
