using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject
{
	public int itemID;
    public string itemName;
	public Sprite itemSprite;
	public ItemType itemType;
	public SpriteLibraryAsset spriteLibraryAsset;
}