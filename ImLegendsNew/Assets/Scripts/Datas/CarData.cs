using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "ScriptableObjects / Car ")]
public class CarData : Data
{
    public string carName;
    [Range(1, 5)] public int carLevel;
    public int price;
    public Properties properties;
}
