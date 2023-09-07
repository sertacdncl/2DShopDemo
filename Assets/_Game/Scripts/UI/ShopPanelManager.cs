using System;
using UnityEngine;
using UnityEngine.Events;

public class ShopPanelManager : Singleton<ShopPanelManager>
{
    private Canvas _canvas;
	public UnityAction OnStoreOpened;
	
	private void Awake()
	{
		_canvas = GetComponent<Canvas>();
	}
	
	public void Show(bool active)
	{
		_canvas.enabled = active;
		if (active)
			OnStoreOpened?.Invoke();
	}
}