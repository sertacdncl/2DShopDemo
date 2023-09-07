using System;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
	public CharacterSlotType slotType;
	public CharacterItem item;

	private void Start()
	{
		UpdateSlotData();
	}

	public void UpdateSlotData()
	{
		var itemData = PlayerVisualManager.Instance.GetEquippedItemData(slotType);
		item.SetItemData(itemData);
	}
    
}