using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Update()
    {
        GetComponent<BoxCollider>().isTrigger = GameManager.instance.monsterMode;
    }
}
