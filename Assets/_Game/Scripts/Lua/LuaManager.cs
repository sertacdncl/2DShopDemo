using UnityEngine;
using XLua;

public class LuaManager
{
	private static LuaEnv _luaEnv;
	public static LuaEnv LuaEnv
	{
		get
		{
			if (_luaEnv == null)
			{
				_luaEnv = new LuaEnv();
				_luaEnv.AddLoader((ref string filename) =>
				{
					var luaScript = Resources.Load<TextAsset>($"Lua/{filename}");
					return luaScript != null ? luaScript.bytes : null;
				});
			}
			return _luaEnv;
		}
	}
	
	public static void Dispose()
	{
		_luaEnv?.Dispose();
		_luaEnv = null;
	}

}