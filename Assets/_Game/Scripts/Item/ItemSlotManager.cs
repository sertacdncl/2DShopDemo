using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemSlotManager : MonoBehaviour
{
	[SerializeField] private AssetReference itemSlotPrefab;
	[SerializeField] private Transform slotParent;

	public int slotCount = 25;
	public ItemSlot[] slots;

	private bool _slotsCreated;

	private void OnEnable()
	{
		PlayerInventory.Instance.OnItemAdded += OnItemAdded;
		PlayerInventory.Instance.OnItemRemoved += OnItemRemoved;
	}

	private void OnDisable()
	{
		var playerInventory = PlayerInventory.Instance;
		if (ReferenceEquals(playerInventory,null))
			return;
		playerInventory.OnItemAdded -= OnItemAdded;
		playerInventory.OnItemRemoved -= OnItemRemoved;
	}

	private void OnItemAdded(ItemObject itemData)
	{
		var slot = GetEmptySlot();
		if (slot != null)
			slot.SetItemData(itemData);
	}

	private void OnItemRemoved(ItemObject itemData)
	{
		foreach (var slot in slots)
		{
			if (slot.UIItem.itemData == itemData)
			{
				slot.RemoveItemData();
				break;
			}
		}
	}

	private void Start()
	{
		slots = new ItemSlot[slotCount];
		CreateSlots();
	}

	private void CreateSlots()
	{
		Addressables.LoadAssetAsync<GameObject>(itemSlotPrefab).Completed += handle =>
		{
			if (handle.Status == AsyncOperationStatus.Succeeded)
			{
				for (int i = 0; i < slotCount; i++)
				{
					var index = i;
					Addressables.InstantiateAsync(itemSlotPrefab, slotParent).Completed += instantiateHandle =>
					{
						if (instantiateHandle.Status == AsyncOperationStatus.Succeeded)
						{
							var slot = instantiateHandle.Result.GetComponent<ItemSlot>();
							slot.SlotIndex = index;
							slots[index] = slot;
							if (index == slotCount - 1)
							{
								_slotsCreated = true;
								InitializeInventorySlots();
							}
						}
					};
				}
			}
		};
	}

	private void InitializeInventorySlots()
	{
		var playerItems = PlayerInventory.Instance.items;
		foreach (var itemData in playerItems)
		{
			var slot = GetEmptySlot();
			if (slot != null)
				slot.SetItemData(itemData);
		}
	}

	private ItemSlot GetEmptySlot()
	{
		foreach (var slot in slots)
		{
			if (!slot.IsSlotUsing)
				return slot;
		}

		return null;
	}
}