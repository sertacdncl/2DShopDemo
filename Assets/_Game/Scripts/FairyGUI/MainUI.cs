using FairyGUI;
using UnityEngine;

public class MainUI : MonoBehaviour
{
	#region References

	[SerializeField] private RenderTexture characterPortrait;

	private GComponent _mainView;
	private GComponent _inventoryView;

	private InventoryUI _inventoryUI;

	#endregion


	private void Awake()
	{
		UIConfig.defaultFont = "Microsoft YaHei UI";
		UIPackage.AddPackage("Inventory/Game");
	}
	
	private void Start()
	{
		_mainView = GetComponent<UIPanel>().ui;


		_inventoryView = _mainView.GetChild("inventory").asCom;
		_inventoryUI = new InventoryUI();
		_inventoryUI.Initialize(_inventoryView);
	}
	
}