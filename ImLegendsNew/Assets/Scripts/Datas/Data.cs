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
	public string itemName;
	public string description;
	public Sprite sprite;
	public GameObject itemPrefab;
}
