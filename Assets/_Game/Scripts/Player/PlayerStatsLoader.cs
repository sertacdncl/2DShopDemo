using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStatsLoader : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI playerMoneyText;

	public void LoadPlayerMoney()
	{
		playerMoneyText.text = new StringBuilder().Append(PlayerCurrencyService.CurrentMoney).ToString();
	}
}
