using UnityEngine;
public class Data : ScriptableObject
{
	public enum ItemType
	{
		chest,
		part,
		car,
		color
	}

	public ItemType itemType;
	public string _name;
	public string description; 
	public GameObject itemPrefab;
}
