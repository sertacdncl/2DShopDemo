using System;
using FairyGUI;
using UnityEngine;

public class MainUI : MonoBehaviour
{
	private GComponent _mainView;
	private GComponent _inventoryView;

	[SerializeField] private RenderTexture characterPortrait;
	
	
	private void Awake()
	{
		UIConfig.defaultFont = "Microsoft YaHei UI";
		UIPackage.AddPackage("Inventory/Game");
		
		PlayerInventory.Instance.OnItemAdded += OnItemAddedInventory;
		PlayerInventory.Instance.OnItemRemoved += OnItemRemovedInventory;
	}

	// private void OnDisable()
	// {
	// 	PlayerInventory.Instance.OnItemAdded -= OnItemAddedInventory;
	// 	PlayerInventory.Instance.OnItemRemoved -= OnItemRemovedInventory;
	// }

	private void Start()
	{
		_mainView = GetComponent<UIPanel>().ui;
		_inventoryView = _mainView.GetChild("inventory").asCom;
		FillInventory();
		LoadCharacterView();
	}
	
	private void OnItemAddedInventory(ItemObject itemObject)
	{
		
	}

	private void OnItemRemovedInventory(ItemObject itemObject)
	{
		
	}

	private void LoadCharacterView()
	{
		GLoader loader = _inventoryView.GetChild("characterView").asLoader;
		InventoryPanelManager.Instance.CharacterPortraitCam.SetActive(true);
		NTexture nTex = new NTexture(characterPortrait);
		loader.texture = nTex;
		
	}
	
	private void FillInventory()
	{
		GList iSlots = _inventoryView.GetChild("inventoryList").asList;
		iSlots.RemoveChildrenToPool();
		
		var items = PlayerInventory.Instance.GetPlayerItems();
		var itemCount = items.Count;
		var itemSlotCount = itemCount + (itemCount % 3 == 0 ? 9 : 6+itemCount%3); // Make sure we have even number of slots for the grid layout
		
		if (itemCount == 0)
		{
			itemSlotCount = 12;	
		}
		
		for (int i = 0; i < itemSlotCount; i++)
		{
			var slot = iSlots.AddItemFromPool();
			slot.name = $"Slot_{i}";
			
			GButton slotBtn = slot.asButton;
			GLoader loader = slotBtn.GetChild("icon").asLoader;
			
			loader.draggable = true;
			loader.texture = null;
			
			if (i < itemCount)
			{
				var itemData = items[i];
				var nTex = new NTexture(itemData.itemSprite);
				
				loader.texture = nTex;
				loader.onDragStart.Add((EventContext context) =>
				{
					context.PreventDefault();
					var data = new DragDataDouble()
					{
						data = slotBtn,
						data2 = itemData // You can set a second data if needed
					};
					DragDropManager.inst.StartDrag(slotBtn, null, data);
					
					var agent = DragDropManager.inst.dragAgent;
					agent.texture = loader.texture;
					agent.fill = FillType.Scale; //To make sure the icon is scaled properly
					agent.SetSize(loader.width, loader.height);
				});
			}
		}
	}
}