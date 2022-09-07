using UnityEngine;

[CreateAssetMenu(fileName = "CarPart" , menuName = "ScriptableObjects / Car Part")]
public class CarPartData : Data
{
	public enum PartTransform
	{
		fBumper,
		bBumper,
		side,
		window,
		cowling,
		rim
	}
	[Header("Car Part Data")]
	public GameObject partModel;
    public PartTransform partTransform;
	public int partTransformIndex;
	[Range(1, 5)] public int partLevel;
	public int price;
	public Properties properties;    
}
