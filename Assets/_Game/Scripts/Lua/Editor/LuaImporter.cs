#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System.IO;

[ScriptedImporter(1, "lua")]
public class LuaImporter : ScriptedImporter
{
	public override void OnImportAsset(AssetImportContext ctx)
	{
		string text = File.ReadAllText(ctx.assetPath);
		var asset = new TextAsset(text);
		ctx.AddObjectToAsset("LuaScript", asset);
		ctx.SetMainObject(asset);
	}
}
#endif