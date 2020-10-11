using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseZoom : MonoBehaviour {
  public CinemachineVirtualCamera virtualCamera;
  public CinemachineComponentBase componentBase;

  public float minZoom;
  public float maxZoom;
  public float scrollScale;
  private float rawZoom;
  private float zoomDistance;

  void Start() {
    virtualCamera = GetComponent<CinemachineVirtualCamera>();
    componentBase = virtualCamera.GetCinemachineComponent<CinemachineComponentBase>();

    zoomDistance = (maxZoom + minZoom) / 2;
    rawZoom = zoomDistance;
  }

  void Update() {
    rawZoom += Input.mouseScrollDelta.y * scrollScale;

    zoomDistance = Mathf.Clamp(rawZoom, minZoom, maxZoom);
    rawZoom = zoomDistance;

    if (componentBase is CinemachineFramingTransposer) {
      (componentBase as CinemachineFramingTransposer).m_CameraDistance = zoomDistance;
    }
  }
}
