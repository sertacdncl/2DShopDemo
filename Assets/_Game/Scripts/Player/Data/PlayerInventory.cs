using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : Singleton<PlayerInventory>
{
	public List<ItemObject> items;
	public UnityAction<ItemObject> OnItemAdded;
	public UnityAction<ItemObject> OnItemRemoved;

	private readonly string PlayerInventoryDataKey = "PlayerInventoryData";

	private void Start()
	{
		LoadPlayerInventory();
	}

	private void SavePlayerInventory()
	{
		var idList = new List<int>();
		items.ForEach(x=>idList.Add(x.itemID));
		var inventoryData = JsonConvert.SerializeObject(idList);
		PlayerPrefs.SetString(PlayerInventoryDataKey, inventoryData);
		PlayerPrefs.Save();
	}
	
	private void LoadPlayerInventory()
	{
		if (!PlayerPrefs.HasKey(PlayerInventoryDataKey))
			return;
		var dataJsonString = PlayerPrefs.GetString(PlayerInventoryDataKey);
		var inventoryItemIdList = JsonConvert.DeserializeObject<List<int>>(dataJsonString);
		foreach (var id in inventoryItemIdList)
		{
			var itemData = ItemManager.Instance.GetItemDataFromId(id);
			AddItem(itemData);
		}
	}
    
	public void AddItem(ItemObject itemObject)
	{
		items.Add(itemObject);
		OnItemAdded?.Invoke(itemObject);
		SavePlayerInventory();
	}

	public void RemoveItem(ItemObject itemData)
	{
		items.Remove(itemData);
		OnItemRemoved?.Invoke(itemData);
		SavePlayerInventory();
	}

	public bool HasItem(ItemObject itemData)
	{
		return items.Contains(itemData);
	}
}
