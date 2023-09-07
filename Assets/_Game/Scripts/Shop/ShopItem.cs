using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
	[SerializeField] private Image itemImage;
	[SerializeField] private TextMeshProUGUI itemPriceText;
	[SerializeField] private Button buyButton;
	[SerializeField] private Button sellButton;
	public ItemObject ItemData { get; set; }
	public int ItemPrice { get; set; }
    
	private void OnEnable()
	{
		ShopPanelManager.Instance.OnStoreOpened += OnStoreOpened;
		PlayerCurrencyService.OnMoneyChanged += OnPlayerMoneyChanged;
	}
	
	private void OnDisable()
	{
		ShopPanelManager.Instance.OnStoreOpened -= OnStoreOpened;
		PlayerCurrencyService.OnMoneyChanged -= OnPlayerMoneyChanged;
	}

	private void OnStoreOpened()
	{
		RefreshStatus();
	}
	
	private void OnPlayerMoneyChanged()
	{
		RefreshStatus();
	}

	private void RefreshStatus()
	{
		buyButton.interactable = PlayerCurrencyService.CurrentMoney >= ItemPrice;
		sellButton.interactable = PlayerInventory.Instance.HasItem(ItemData);
	}

	public void SetupItem(ItemObject newItemData, int itemPrice)
	{
		ItemData = newItemData;
		itemImage.sprite = ItemData.itemSprite;
		var sb = new StringBuilder().Append(itemPrice);
		itemPriceText.text = sb.ToString();
		ItemPrice = itemPrice;
	}

	public void BuyItem()
	{
		if(ReferenceEquals(ItemData,null))
			return;
		if(PlayerCurrencyService.CurrentMoney<ItemPrice)
			return;
		
		PlayerInventory.Instance.AddItem(ItemData);
		SoundManager.Instance.PlayBuySellSound();
		ShopManager.Instance.ShowBuySellText(false);
		PlayerCurrencyService.AddMoney(-ItemPrice);
	}

	public void SellItem()
	{
		if(ReferenceEquals(ItemData,null))
			return;
		if(!PlayerInventory.Instance.HasItem(ItemData))
			return;
		
		PlayerInventory.Instance.RemoveItem(ItemData);
		SoundManager.Instance.PlayBuySellSound();
		ShopManager.Instance.ShowBuySellText(true);
		PlayerCurrencyService.AddMoney(ItemPrice);
	}
}