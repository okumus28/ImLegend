using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform carHolder;
    public Transform target;
    public float trailDistance = 5.0f;
    public float heightOffset = 3.0f;
    public float cameraDelay = 0.02f;

    [SerializeField]Vector3 offset;

    private void Start()
    {
        //target = PlayerBase.GetComponent<PlayerBase>().vehicleController.transform;
        offset = transform.position;
        target = carHolder.GetChild(PlayerPrefs.GetInt("CurrentCarIndex"));
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver)
        {
            transform.position += Vector3.forward / 15;
            return;
        }
        Vector3 followPos = target.GetChild(0).position - target.GetChild(0).forward * trailDistance;

        followPos.y += heightOffset;

        transform.position = new Vector3(0, offset.y, target.position.z + offset.z);
    }
}
