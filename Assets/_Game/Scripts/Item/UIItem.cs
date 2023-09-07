using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIItem : MonoBehaviour
{
	#region References

	public ItemObject itemData;
	
	private Image _itemImage;
	private RectTransform _rectTransform;
	private Canvas _canvas;

	#endregion

	
	private Vector2 _offset;
	private Vector2 _basePosition;

	private void Start()
	{
		_rectTransform = GetComponent<RectTransform>();
		_itemImage = GetComponent<Image>();
		_canvas = GetComponentInParent<Canvas>();
		_basePosition = _rectTransform.anchoredPosition;
	}

	public void SetItemData(ItemObject newItemData)
	{
		itemData = newItemData;
		_itemImage.enabled = true;
		_itemImage.sprite = itemData.itemSprite;
	}
	
	public void OnPointerDown(BaseEventData eventData)
	{
		if (GlobalUIItemEvents.IsDragging)
			return;
		GlobalUIItemEvents.IsDragging = true;
		GlobalUIItemEvents.UIItem = this;
		_itemImage.raycastTarget = false;
		_canvas.overrideSorting = true;
		
		_offset = (Vector2)_rectTransform.position - eventData.currentInputModule.input.mousePosition;
	}

	public void OnDrag(BaseEventData eventData)
	{
		_rectTransform.position = eventData.currentInputModule.input.mousePosition + _offset;
	}

	public void OnPointerUp(BaseEventData eventData)
	{
		GlobalUIItemEvents.IsDragging = false;

		if (GlobalUIItemEvents.CharacterSlot != null)
		{
			if (GlobalUIItemEvents.UIItem.itemData.itemID != GlobalUIItemEvents.CharacterSlot.item.ItemData.itemID)
			{
				if (IsItemSuitableToSlot(itemData.itemType, GlobalUIItemEvents.CharacterSlot.slotType))
				{
					var newEquipTempData = itemData;
					var equippedItemTempData = GlobalUIItemEvents.CharacterSlot.item.ItemData;
					PlayerVisualManager.Instance.EquipItem(newEquipTempData.itemID, GlobalUIItemEvents.CharacterSlot.slotType);
					GlobalUIItemEvents.CharacterSlot.UpdateSlotData();
					PlayerInventory.Instance.RemoveItem(itemData);
					PlayerInventory.Instance.AddItem(equippedItemTempData);
				}
			}
		}
		_rectTransform.anchoredPosition = _basePosition;
		_canvas.overrideSorting = false;
		_itemImage.raycastTarget = true;
		
	}

	public void RemoveItemData()
	{
		itemData = null;
		_itemImage.enabled = false;
		_itemImage.sprite = null;
	}

	private bool IsItemSuitableToSlot(ItemType itemType, CharacterSlotType slotType)
	{
		return slotType switch
		{
			CharacterSlotType.Chest => itemType == ItemType.Body,
			CharacterSlotType.Leg => itemType == ItemType.Leg,
			CharacterSlotType.Foot => itemType == ItemType.Foot,
			_ => false
		};
	}
}
