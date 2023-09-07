using System;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
	public int SlotIndex { get; set; }
	public UIItem UIItem { get; set; }
    
	public bool IsSlotUsing => !ReferenceEquals(UIItem.itemData, null);

	private void Awake()
	{
		UIItem = GetComponentInChildren<UIItem>();
	}

	public void SetItemData(ItemObject newItemData)
	{
		UIItem.SetItemData(newItemData);
	}
    
	public void RemoveItemData()
	{
		UIItem.RemoveItemData();
	}
}
