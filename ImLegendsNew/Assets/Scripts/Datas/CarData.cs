using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "ScriptableObjects / Car ")]
public class CarData : ScriptableObject
{
    public string carName;
    public string description;
    public int price;
    [Range(1, 5)] public int carLevel;
    public Properties properties;
}
