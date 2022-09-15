using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public GameObject[] roads;
    public Transform firstRoad;

    public static RoadController instance;
    private void Awake()
    {
        instance = this;
    }

    public void RoadCreate()
    {
        GameObject road = Instantiate(roads[Random.Range(0, roads.Length)], firstRoad.position + new Vector3(0, 0, 160) , Quaternion.identity , transform);
        firstRoad = road.transform;
        Destroy(transform.GetChild(0).gameObject, 1.5f);
    }
}
