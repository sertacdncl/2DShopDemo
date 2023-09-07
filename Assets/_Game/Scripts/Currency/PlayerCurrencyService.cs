using UnityEngine;
using UnityEngine.Events;

public static class PlayerCurrencyService
{
	public static int CurrentMoney
	{
		get
		{
			if(!_isLoaded)
				LoadPlayerCurrencyData();
			return _currentMoney;
		}
		private set
		{
			_currentMoney = value;
			PlayerPrefs.SetInt(_key, _currentMoney);
			PlayerPrefs.Save();
			OnMoneyChanged?.Invoke();
		}
	}

	public static UnityAction OnMoneyChanged;
	private static int _currentMoney;
	private static int _startMoney = 50;
	private static bool _isLoaded;
	private static string _key = "PlayerCurrency";
    
	private static void LoadPlayerCurrencyData()
	{
		if (PlayerPrefs.HasKey(_key))
			_currentMoney = PlayerPrefs.GetInt(_key);
		else
		{
			_currentMoney = _startMoney;
			PlayerPrefs.SetInt(_key, _currentMoney);
			PlayerPrefs.Save();
		}

		_isLoaded = true;
	}

	public static void AddMoney(int money)
	{
		CurrentMoney += money;
	
	}
}