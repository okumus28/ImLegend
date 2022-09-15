using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform carHolder;
    [SerializeField] Car currentCar;

    public  Properties properties;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        currentCar = carHolder.GetChild(PlayerPrefs.GetInt("CurrentCarIndex")).GetComponent<Car>();
    }

    private void Start()
    {
        properties = currentCar.properties;
    }
}
