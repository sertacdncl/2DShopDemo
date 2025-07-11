using System;
using UnityEngine;
using XLua;

[CSharpCallLua]
public delegate void LuaPlayerUpdate(float deltaTime, Vector2 joystickInput);



public class PlayerControllerLua : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private LuaTable _playerLua;
	private Vector2 _joystickInput;
	
	private Action<Vector2> _onMoveHandler;
	private Action _onEndHandler;

	private void Start()
	{
		LuaEnv luaEnv = LuaManager.LuaEnv;
		var luaScript = Resources.Load<TextAsset>("Lua/player");
		_playerLua = luaEnv.DoString(luaScript.text, "player")[0] as LuaTable;

		if (_playerLua == null)
		{
			Debug.LogError("Failed to load Player Lua script.");
			return;
		}

		_onMoveHandler = dir => _joystickInput = dir;
		_onEndHandler = () => _joystickInput = Vector2.zero;
		JoystickEvents.OnMove += _onMoveHandler;
		JoystickEvents.OnEnd += _onEndHandler;

		_playerLua.Set("rb", GetComponent<Rigidbody2D>());
		_playerLua.Set("position", (Vector2)transform.position);
		_playerLua.Set("animator", animator);
	}

	private void FixedUpdate()
	{
		_playerLua.Get<LuaPlayerUpdate>("update")?.Invoke(Time.deltaTime, _joystickInput);
	}

	private void OnDestroy()
	{
		_playerLua?.Dispose();
		JoystickEvents.OnMove -= _onMoveHandler;
		JoystickEvents.OnEnd -= _onEndHandler;
	}
}