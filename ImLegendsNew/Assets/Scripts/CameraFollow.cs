using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float trailDistance = 5.0f;
    public float heightOffset = 3.0f;
    public float cameraDelay = 0.02f;

    private void Start()
    {
        //target = PlayerBase.GetComponent<PlayerBase>().vehicleController.transform;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 followPos = target.GetChild(0).position - target.GetChild(0).forward * trailDistance;

        followPos.y += heightOffset;

        transform.position = new Vector3(0, 3.5f, target.position.z - 10f);
    }
}