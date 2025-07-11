using System.Collections;
using FairyGUI;
using UnityEngine;
using XLua;

public class LuaEntry : MonoBehaviour
{
	private LuaEnv luaEnv;

	private IEnumerator Start()
	{
		while (GRoot.inst == null)
			yield return null;
		
		Application.targetFrameRate = 60;
		GRoot.inst.SetContentScaleFactor(1280, 720, UIContentScaler.ScreenMatchMode.MatchHeight);
		
		yield return null;
		luaEnv = LuaManager.LuaEnv;
		
		
		var luaScript = Resources.Load<TextAsset>("Lua/main");
		luaEnv.DoString(luaScript.text, "main");
	}

	private void OnDestroy()
	{
		LuaManager.Dispose();
	}
}