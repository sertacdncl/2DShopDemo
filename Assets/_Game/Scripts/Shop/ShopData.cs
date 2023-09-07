using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Data", menuName = "Shop/Data")]
public class ShopData : ScriptableObject
{
	public List<SellableItems> sellableItems;
}

[Serializable]
public struct SellableItems
{
	public ItemObject itemData;
	public int price;
}