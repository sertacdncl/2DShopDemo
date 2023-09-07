using System;
using System.Collections.Generic;

public class PlayerVisualManager : Singleton<PlayerVisualManager>
{
	public List<Equipment> equipmentList;

	private void Awake()
	{
		LoadEquipmentList();
	}

	private void LoadEquipmentList()
	{
		var equippedItems= transform.GetComponentsInChildren<PlayerEquippedItem>();
		foreach (var equippedItem in equippedItems)
		{
			var equipment = new Equipment()
			{
				slotType = equippedItem.slotType,
				equippedItem = equippedItem
			};
			equipmentList.Add(equipment);
		}
	}

	public ItemObject GetEquippedItemData(CharacterSlotType slotType)
	{
		var index = equipmentList.FindIndex(x=>x.slotType == slotType);
		if (index == -1)
			return null;
		return ItemManager.Instance.GetItemDataFromId(equipmentList[index].equippedItem.EquippedId);
	}

	private List<PlayerEquippedItem> GetEquipSlots(CharacterSlotType slotType)
	{
		return equipmentList.FindAll(x=>x.slotType == slotType).ConvertAll(x=>x.equippedItem);
	}
	
	private PlayerEquippedItem GetEquipSlot(CharacterSlotType slotType)
	{
		var index = equipmentList.FindIndex(x=>x.slotType == slotType);
		return index == -1 ? null : equipmentList[index].equippedItem;
	}
	
	public void EquipItem(int itemId, CharacterSlotType slotType)
	{
		var equipSlots = GetEquipSlots(slotType);
		equipSlots.ForEach(x=>x.SetEquippedItemId(itemId));
	}
}

[Serializable]
public class Equipment
{
	public CharacterSlotType slotType;
	public PlayerEquippedItem equippedItem;
}