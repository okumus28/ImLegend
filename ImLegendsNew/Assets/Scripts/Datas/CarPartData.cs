using UnityEngine;

[CreateAssetMenu(fileName = "CarPart" , menuName = "ScriptableObjects / Car Part")]
public class CarPartData : ScriptableObject
{
	enum PartTransform
	{
		fBumper,
		bBumper,
		side,
		window,
		cowling,
		rim
	}

	[SerializeField] PartTransform partTransform;
	public string partName;
	[Range(1, 5)] public int partLevel;
	public int price;
	public Properties properties;    
}
