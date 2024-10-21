using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private float offset;

    void Awake() {
        offset = transform.position.y - player.transform.position.y;
    }

    void Update() {
        Vector3 currentPosition = transform.position;
        currentPosition.y = player.transform.position.y + offset;
        transform.position = currentPosition;
    }
}
