using System;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class InventoryPanelManager : Singleton<InventoryPanelManager>
{
	[SerializeField] private GameObject characterPortraitCamera;
	[SerializeField] private PlayerStatsLoader playerStatsLoader;
	public Canvas panelCanvas;

	private void Awake()
	{
		panelCanvas = GetComponent<Canvas>();
	}

	public void Show(bool active)
	{
		characterPortraitCamera.SetActive(active);
		panelCanvas.enabled = active;
		if(active)
			playerStatsLoader.LoadPlayerMoney();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			Show(!panelCanvas.enabled);
		}
	}
}
