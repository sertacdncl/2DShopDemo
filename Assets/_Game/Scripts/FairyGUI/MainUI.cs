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
	}

	private void Start()
	{
		_mainView = GetComponent<UIPanel>().ui;
		_inventoryView = _mainView.GetChild("inventory").asCom;
		FillInventory();
		LoadCharacterView();
	}

	private void LoadCharacterView()
	{
		GLoader loader = _inventoryView.GetChild("characterView").asLoader;
		InventoryPanelManager.Instance.CharacterPortraitCam.SetActive(true);
		NTexture nTex = new NTexture(characterPortrait);
		loader.texture = nTex;
		loader.SetSize(characterPortrait.width, characterPortrait.height);
	}
	
	private void FillInventory()
	{
		GList iSlots = _inventoryView.GetChild("inventoryList").asList;
		iSlots.RemoveChildrenToPool();

		var itemSlotCount = 10;
		for (int i = 0; i < itemSlotCount; i++)
		{
			GButton item = iSlots.AddItemFromPool().asButton;
			item.draggable = true;
			item.onDragStart.Add((EventContext context) =>
			{
				//Cancel the original dragging, and start a new one with a agent.
				context.PreventDefault();

				DragDropManager.inst.StartDrag(item, item.icon, item.icon, (int)context.data);
			});
			// var icon = item.GetChild("icon");
			// icon.draggable = true;
			// var iconLoader = item.GetChild("icon").asLoader;
			
			
		}
	}
}