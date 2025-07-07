using FairyGUI;
using UnityEngine;

public class MainUI : MonoBehaviour
{
	#region References

	private GComponent _mainView;
	private GComponent _inventoryView;
	private GComponent _joystickView;

	private JoystickUI _joystickUI;
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
		
		_joystickUI = new JoystickUI();
		_joystickUI.Initialize(_mainView);
		
		_inventoryView = _mainView.GetChild("inventoryComponent").asCom;
		_inventoryUI = new InventoryUI();
		_inventoryUI.Initialize(_inventoryView);
	}
}