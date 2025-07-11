using UnityEngine;
using XLua;

[LuaCallCSharp]
public static class CheatCommands
{
	public static void AddGold(int amount)
	{
		// Implement the logic to add gold to the player's account
		// This is a placeholder implementation
		Debug.Log($"Added {amount} gold to the player's account.");
		PlayerCurrencyService.AddMoney(amount);
	}
	
}