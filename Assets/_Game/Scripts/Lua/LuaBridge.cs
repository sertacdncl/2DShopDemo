using UnityEngine;
using XLua;

[LuaCallCSharp]
public delegate void SimpleCallback();


[LuaCallCSharp]
public class LuaBridge
{
	public static void PrintHello()
	{
		// This method can be called from Lua scripts to print "Hello from C#"
		Debug.Log("Hello from Lua");
	}
}