using UnityEngine;
using XLua;

[CSharpCallLua]
public delegate void LuaPlayerUpdate(float deltaTime, Vector2 joystickInput);

public class PlayerControllerLua : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private LuaTable _playerLua;
	private Vector2 _joystickInput;

	private void Start()
	{
		LuaEnv luaenv = new LuaEnv();
		var luaScript = Resources.Load<TextAsset>("Lua/player");
		_playerLua = luaenv.DoString(luaScript.text, "player")[0] as LuaTable;
		
		if(_playerLua == null)
		{
			Debug.LogError("Failed to load Player Lua script.");
			return;
		}
		
		JoystickEvents.OnMove += dir => _joystickInput = dir;
		JoystickEvents.OnEnd += () => _joystickInput = Vector2.zero;
		
		_playerLua.Set("rb", GetComponent<Rigidbody2D>());
		_playerLua.Set("position", (Vector2)transform.position);
		_playerLua.Set("animator", animator);
	}

	private void FixedUpdate()
	{
		_playerLua.Get<LuaPlayerUpdate>("update")?.Invoke(Time.deltaTime, _joystickInput);
	}
}