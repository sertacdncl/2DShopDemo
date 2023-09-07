using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(SpriteLibrary))]
public class PlayerEquippedItem : MonoBehaviour
{
	public CharacterSlotType slotType;
	private SpriteLibrary _spriteLibrary;
	[SerializeField] private int equippedId;//Default assigned in inspector
	public int EquippedId
	{
		get => equippedId;
		private set
		{
			equippedId = value;
			PlayerPrefs.SetInt(GetPlayerPrefsKey(), value);
			OnEquippedItemIdChanged?.Invoke(value);
		}
	}
	public UnityAction<int> OnEquippedItemIdChanged;

	private string GetPlayerPrefsKey()
	{
		var stringBuilder = new StringBuilder();
		stringBuilder.Append("EquippedItem_");
		stringBuilder.Append(slotType);
		var key = stringBuilder.ToString();
		return key;
	}

	private void OnEnable() => OnEquippedItemIdChanged += OnItemIdChange;

	private void OnDisable() => OnEquippedItemIdChanged -= OnItemIdChange;

	private void Awake()
	{
		_spriteLibrary = GetComponent<SpriteLibrary>();
		LoadPreviousItem();
	}

	private void LoadPreviousItem()
	{
		var key = GetPlayerPrefsKey();
		if (PlayerPrefs.HasKey(key))
		{
			var itemId = PlayerPrefs.GetInt(key);
			SetEquippedItemId(itemId);
		}
		else
		{
			SetEquippedItemId(EquippedId);
		}
		OnItemIdChange(EquippedId);
	}

	public void SetEquippedItemId(int itemId)
	{
		EquippedId = itemId;
	}

	private void OnItemIdChange(int id)
	{
		var item = ItemManager.Instance.GetItemDataFromId(EquippedId);
		_spriteLibrary.spriteLibraryAsset = item.spriteLibraryAsset;
	}
}