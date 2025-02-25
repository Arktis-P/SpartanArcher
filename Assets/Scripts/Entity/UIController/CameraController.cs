using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;  // target == player

    private float cameraMinX = 0f;
    private float cameraMaxX = 18f;
    private float cameraY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) { Debug.LogError("Cannot find Traget Object!"); return; }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;  // get camera position
        pos.x = target.transform.position.x;
        pos.y = cameraY;
        // lock position
        if (pos.x <= cameraMinX) pos.x = cameraMinX;
        if (pos.x >= cameraMaxX) pos.x = cameraMaxX;

        transform.position = pos;
    }
}
