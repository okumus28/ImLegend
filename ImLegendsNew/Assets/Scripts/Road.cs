using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject[] zombies;

    [SerializeField] Transform spawnBounds;
    // Start is called before the first frame update
    void Start()
    {
        for (int k = 0; k < 4; k++)
        {
            CreateZombi(k);
            CreateObstacle(k);
            CreateItem(k);
        }
    }

    void CreateItem(int index)
    {
        if (Random.Range(0, 100) <= 40)
        {
            int random = Random.Range(0, items.Length);
            Instantiate(items[random], RandomizePosition(spawnBounds.GetChild(index), 2f), Quaternion.identity, transform);
        }
    }

    void CreateObstacle(int index)
    {
        for (int i = 0; i < Random.Range(0, 2); i++)
        {
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], RandomizePosition(spawnBounds.GetChild(index), 0), Quaternion.identity, transform);
        }
    }

    void CreateZombi(int index)
    {
        for (int i = 0; i < Random.Range(0, 5); i++)
        {
            Zombie zombie = Instantiate(zombies[Random.Range(0, zombies.Length)], RandomizePosition(spawnBounds.GetChild(index), 0), Quaternion.identity, transform).GetComponent<Zombie>();
            zombie.boundsCollider = spawnBounds.GetChild(index).GetComponent<BoxCollider>();
        }
    }
   

    public Vector3 RandomizePosition(Transform spawnCollider , float y)
    {
        Bounds bounds = spawnCollider.GetComponent<BoxCollider>().bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}
