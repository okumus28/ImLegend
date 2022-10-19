using UnityEngine;

public class SpecialItems : MonoBehaviour
{
    public static SpecialItems Instance;

    public GameObject[] rocketPrefab;
    public GameObject bombPrefab;
    public GameObject discoPrefab;

    private void Awake()
    {
        Instance = this;
    }
}
