using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
	private CharacterSlot _linkedSlot;
	private Image _itemImage;
	public ItemObject ItemData { get; set; }

	private void Awake()
	{
		_linkedSlot = GetComponentInParent<CharacterSlot>();
		_itemImage = GetComponent<Image>();
	}

	public void SetItemData(ItemObject newItemData)
	{
		ItemData = newItemData;
		_itemImage.sprite = ItemData.itemSprite;
	}
	
    public void OnPointerEnter()
	{
		if(!GlobalUIItemEvents.IsDragging)
			return;
        
		GlobalUIItemEvents.CharacterSlot = _linkedSlot;
	}

	public void OnPointerExit()
	{
		if (GlobalUIItemEvents.IsDragging)
		{
			GlobalUIItemEvents.CharacterSlot = null;
		}
	}
}
