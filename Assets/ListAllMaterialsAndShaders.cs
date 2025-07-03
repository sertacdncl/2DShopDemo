using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ListAllMaterialsAndShaders : EditorWindow
{
    [MenuItem("Tools/Diagnose Materials & Shaders")]
    public static void ShowWindow()
    {
        GetWindow<ListAllMaterialsAndShaders>("Material Diagnostic");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("List All Materials and Their Shaders"))
        {
            DiagnoseMaterials();
        }
    }

    private void DiagnoseMaterials()
    {
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");
        int total = 0;
        int urp = 0;
        int builtIn = 0;
        int unknown = 0;
        int customShaderGraph = 0;

        Dictionary<string, int> shaderUsage = new Dictionary<string, int>();

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat == null || mat.shader == null)
            {
                Debug.LogWarning($"⚠️ Material has no shader: {path}");
                unknown++;
                continue;
            }

            total++;
            string shaderName = mat.shader.name;
            string shaderNameLower = shaderName.ToLower();

            // Shader sayımını yap
            if (!shaderUsage.ContainsKey(shaderName))
                shaderUsage[shaderName] = 1;
            else
                shaderUsage[shaderName]++;

            // Tür kontrolü
            if (shaderNameLower.Contains("universal") || shaderNameLower.Contains("urp"))
            {
                urp++;
            }
            else if (shaderNameLower.Contains("standard") || shaderNameLower.Contains("legacy"))
            {
                builtIn++;
            }
            else if (shaderNameLower.Contains("shader graph") || shaderNameLower.StartsWith("shader graphs"))
            {
                customShaderGraph++;
                Debug.Log($"🧪 Shader Graph detected: {shaderName} — {path}");
            }
            else
            {
                unknown++;
                Debug.LogWarning($"❓ Unknown/Custom Shader: {shaderName} — {path}");
            }
        }

        // Özet
        Debug.Log("🔍 Material Shader Analysis Summary:");
        Debug.Log($"🔢 Total materials: {total}");
        Debug.Log($"🎯 URP shaders: {urp}");
        Debug.Log($"🧱 Built-in (Standard/Legacy): {builtIn}");
        Debug.Log($"🧪 Shader Graph custom shaders: {customShaderGraph}");
        Debug.Log($"❓ Unknown or non-standard: {unknown}");

        Debug.Log("📋 Shader Usage Breakdown:");
        foreach (var pair in shaderUsage)
        {
            Debug.Log($"• {pair.Key} — {pair.Value} material(s)");
        }
    }
}
