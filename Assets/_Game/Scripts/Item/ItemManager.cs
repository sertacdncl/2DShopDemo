using System.Collections.Generic;

public class ItemManager : Singleton<ItemManager>
{
	public List<ItemObject> itemDataList;

	public ItemObject GetItemDataFromId(int itemID)
	{
		var index = itemDataList.FindIndex(x => x.itemID == itemID);
		return index == -1 ? null : itemDataList[index];
	}
	
}