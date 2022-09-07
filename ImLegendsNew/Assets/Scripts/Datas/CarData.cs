using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "ScriptableObjects / Car ")]
public class CarData : Data
{
    [Header("Car Data")]
    public int price;
    [Range(1, 5)] public int carLevel;
    //public string description;
    public Properties properties;
}
