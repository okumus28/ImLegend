//using Microsoft.Unity.VisualStudio.Editor;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects / Item")]
public class Item : ScriptableObject
{
	public enum ItemType
	{
		chest,
		part,
		car,
		color
	}
	public ItemType itemType;
	[Header("Item")]
	public Sprite sprite;
	public GameObject itemPrefab;
}
