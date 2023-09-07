using System;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Extensions.Vectors;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ShopManager : Singleton<ShopManager>
{
	[SerializeField] private AssetReference shopItemAssetRef;
	[SerializeField] private Transform itemsParent;
	[SerializeField] private ShopData shopData;
	[SerializeField] private TextMeshProUGUI buyText;

	private string _buyText = "Item successfully added to Inventory";
	private string _sellText = "Item successfully removed from Inventory";
	private Tween _floatingTween;
	private List<ShopItem> _shopItems;

	private void Start()
	{
		_shopItems = new List<ShopItem>();
		CreateSlots();
	}

	private void CreateSlots()
	{
		Addressables.LoadAssetAsync<GameObject>(shopItemAssetRef).Completed += handle =>
		{
			if (handle.Status == AsyncOperationStatus.Succeeded)
			{
				for (int i = 0; i < shopData.sellableItems.Count; i++)
				{
					var index = i;
					Addressables.InstantiateAsync(shopItemAssetRef, itemsParent).Completed += instantiateHandle =>
					{
						if (instantiateHandle.Status == AsyncOperationStatus.Succeeded)
						{
							var slot = instantiateHandle.Result.GetComponent<ShopItem>();
							_shopItems.Add(slot);
							var sellableItem = shopData.sellableItems[index];
							slot.SetupItem(sellableItem.itemData, sellableItem.price);
						}
					};
				}
			}
		};
	}

	public void ShowBuySellText(bool isSell)
	{
		var buyTextRect = buyText.rectTransform;
        buyTextRect.anchoredPosition = Vector2.zero;
		buyText.DOFade(1, 00.1f);
		buyText.text = isSell ? _sellText : _buyText;
		_floatingTween?.Kill();
		_floatingTween = DOTween.Sequence()
			.Append(buyTextRect.DOMoveY(buyTextRect.anchoredPosition.y + 500, 2f))
			.Join(buyText.DOFade(0, 2f));
	}
}
