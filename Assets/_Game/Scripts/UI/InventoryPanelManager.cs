using System;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class InventoryPanelManager : Singleton<InventoryPanelManager>
{
	[SerializeField] private GameObject characterPortraitCamera;
	public Canvas panelCanvas;

	private void Awake()
	{
		panelCanvas = GetComponent<Canvas>();
	}

	public void Show(bool active)
	{
		characterPortraitCamera.SetActive(active);
		panelCanvas.enabled = active;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			Show(!panelCanvas.enabled);
		}
	}
}
