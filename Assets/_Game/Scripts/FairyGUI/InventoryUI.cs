using System;
using FairyGUI;
using UnityEngine;

[Serializable]
public class InventoryUI
{
	private GList _inventoryList;

	public InventoryUI()
	{
		
	}
	
	
	private void OnItemAddedInventory(ItemObject itemObject)
	{
	}

	private void OnItemRemovedInventory(ItemObject itemObject)
	{
	}

	public void Initialize(GComponent inventoryView)
	{
		_inventoryList = inventoryView.GetChild("inventoryList").asList;
		LoadCharacterView(inventoryView);
		CreateInventory();
		FillInventory();
		
		//Subscribe to the PlayerInventory events
		if (PlayerInventory.Instance != null)
		{
			PlayerInventory.Instance.OnItemAdded += OnItemAddedInventory;
			PlayerInventory.Instance.OnItemRemoved += OnItemRemovedInventory;
		}
		else
		{
			Debug.LogWarning("PlayerInventory instance is null, events will not be subscribed.");
		}
	}
	
	private void LoadCharacterView(GComponent inventoryView)
	{
		GLoader loader = inventoryView.GetChild("characterView").asLoader;
		InventoryPanelManager.Instance.CharacterPortraitCam.SetActive(true);
		var characterPortrait = InventoryPanelManager.Instance.CharacterPortraitRenderTex;
		NTexture nTex = new NTexture(characterPortrait);
		loader.texture = nTex;
	}
	
	private void CreateInventory()
	{
		// Clear the inventory list to start fresh
		_inventoryList.RemoveChildrenToPool();

		// Get the items from the player inventory
		var items = PlayerInventory.Instance.GetPlayerItems();
		var itemCount = items.Count;
		var itemSlotCount =
			itemCount + (itemCount % 3 == 0
				? 9
				: 6 + itemCount % 3); // Make sure we have even number of slots for the grid layout

		if (itemCount == 0)
			itemSlotCount = 12;

		for (int i = 0; i < itemSlotCount; i++)
		{
			var slot = _inventoryList.AddItemFromPool();
			slot.name = $"InventorySlot_{i}";

			GButton slotBtn = slot.asButton;
			GLoader loader = slotBtn.GetLoader();

			//Set default properties
			loader.draggable = true;
			loader.texture = null;
		}
	}

	private void FillInventory()
	{
		var slots = _inventoryList.GetChildren();
		for (var i = 0; i < slots.Length; i++)
		{
			var slot = slots[i];
			var items = PlayerInventory.Instance.GetPlayerItems();
			var itemCount = items.Count;

			//If we have items, set the texture and data
			if (i < itemCount)
			{
				CreateItemToSlot(slot, items[i]);
				SetSlotDragable(slot, items[i]);
			}
			else
				SetSlotDragable(slot, null);

			SetSlotDropable(slot);
		}
	}

	private void CreateItemToSlot(GObject slot, ItemObject itemData)
	{
		var loader = slot.GetLoader();
		loader.texture = itemData.itemSprite.GetNTexture();
		loader.data = itemData; // Store the item data in the loader for later use
		var data = new DragDataDouble()
		{
			data = slot,
			data2 = itemData // You can set a second data if needed
		};
		slot.data = data; // Store the data in the loader for later use
	}

	private static void ClearSlot(GObject slot)
	{
		slot.data = null; // Clear the data from the old loader
		var loader = slot.GetLoader();
		loader.texture = null;
		loader.draggable = false; // Disable dragging from the old slot
	}

	private static void SetSlotDragable(GObject slot, ItemObject itemData)
	{
		var loader = slot.GetLoader();
		loader.draggable = true;

		if (ReferenceEquals(itemData, null))
			return;

		loader.onDragStart.Add((EventContext context) =>
		{
			context.PreventDefault();

			DragDropManager.inst.StartDrag(slot, null, slot.data);

			var agent = DragDropManager.inst.dragAgent;
			agent.texture = loader.texture;
			agent.fill = FillType.Scale; //To make sure the icon is scaled properly
			agent.SetSize(loader.width, loader.height);
		});
	}

	private void SetSlotDropable(GObject slot)
	{
		var slotBtn = slot.asButton;
		slotBtn.onDrop.Add((EventContext context) =>
		{
			var dragData = (DragDataDouble)context.data;
			if (dragData is { data: GObject oldSlot, data2: ItemObject draggedItem })
			{
				if (!ReferenceEquals(slot.data, null))
				{
					SwitchSlots(oldSlot, slot);
					return;
				}
				
				
				CreateItemToSlot(slot, draggedItem);
				SetSlotDragable(slot, draggedItem);
				ClearSlot(oldSlot);
			}
		});
	}
	
	private void SwitchSlots(GObject slot1, GObject slot2)
	{
		var itemData1 = (ItemObject)slot1.GetLoader().data;
		var itemData2 = (ItemObject)slot2.GetLoader().data;

		ClearSlot(slot1);
		ClearSlot(slot2);

		CreateItemToSlot(slot1, itemData2);
		CreateItemToSlot(slot2, itemData1);

		SetSlotDragable(slot1, itemData2);
		SetSlotDragable(slot2, itemData1);
	}
}