using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class multi_cam_movement : MonoBehaviour
{   
    private stat_vault vault_access;
    List<Transform> targets;

    public float smoothTime = .5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimit = 50f;

    private Vector3 velocity;

    Bounds target_bounds;

    Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
        vault_access = GameObject.FindGameObjectWithTag("STATUS").GetComponent<stat_vault>();
    }

    void LateUpdate()
    {
        targets = vault_access.players;
        if (targets.Count == 0 || vault_access.NUM_OF_PLAYERS == 0)
            return;
        target_bounds = GetBounds();
        Vector3 point = targets.Count > 1 ? target_bounds.center : targets[0].position;
        float dist = targets.Count > 1 ? Mathf.Max(target_bounds.size.x, target_bounds.size.y) : 0;
        CameraMove(point);
        CameraZoom(dist);
    }

    private void CameraZoom(float dist)
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, dist / zoomLimit);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    private void CameraMove(Vector3 point)
    {
        transform.position = Vector3.SmoothDamp(transform.position, point + Vector3.back, ref velocity, smoothTime);
    }

    private Bounds GetBounds()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds;
    }
}
