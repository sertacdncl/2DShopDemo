using UnityEditor;
using UnityEngine;
using System.IO;

public class ConvertURPToBuiltInWithBackup : EditorWindow
{
    [MenuItem("Tools/Convert URP Materials to Built-in (with Backup)")]
    public static void ShowWindow()
    {
        GetWindow<ConvertURPToBuiltInWithBackup>("URP to Built-in Converter");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Convert All URP Materials to Built-in (with Backup)"))
        {
            if (EditorUtility.DisplayDialog("Confirm Conversion", "This will modify all URP materials and create backups.\n\nProceed?", "Yes", "Cancel"))
            {
                ConvertMaterials();
            }
        }
    }

    private void ConvertMaterials()
    {
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");
        int converted = 0;
        int skipped = 0;

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat == null || mat.shader == null)
                continue;

            string shaderName = mat.shader.name.ToLower();

            // Daha kapsamlı URP shader kontrolü
            bool isURPShader = shaderName.Contains("universal render pipeline") ||
                               shaderName.Contains("shader graphs") ||
                               shaderName.Contains("urp") ||
                               shaderName.Contains("simple lit") ||
                               shaderName.Contains("lit") ||
                               shaderName.Contains("unlit");

            if (isURPShader)
            {
                // Backup oluştur
                string backupPath = path.Replace(".mat", ".mat.backup");
                if (!File.Exists(backupPath))
                {
                    File.Copy(path, backupPath);
                }

                // Mevcut URP değerlerini al
                Color baseColor = Color.white;
                Texture baseMap = null;
                float cutoff = 0.5f;
                Color emission = Color.black;

                if (mat.HasProperty("_BaseColor"))
                    baseColor = mat.GetColor("_BaseColor");
                if (mat.HasProperty("_BaseMap"))
                    baseMap = mat.GetTexture("_BaseMap");
                if (mat.HasProperty("_Cutoff"))
                    cutoff = mat.GetFloat("_Cutoff");
                if (mat.HasProperty("_EmissionColor"))
                    emission = mat.GetColor("_EmissionColor");

                // Shader değiştir
                mat.shader = Shader.Find("Standard");

                // Yeni shader'a değerleri uygula
                if (mat.HasProperty("_Color"))
                    mat.SetColor("_Color", baseColor);
                if (mat.HasProperty("_MainTex") && baseMap != null)
                    mat.SetTexture("_MainTex", baseMap);
                if (mat.HasProperty("_Cutoff"))
                    mat.SetFloat("_Cutoff", cutoff);

                if (emission.maxColorComponent > 0.01f)
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", emission);
                }

                // Şeffaflık modu
                if (mat.HasProperty("_Surface"))
                {
                    int surface = (int)mat.GetFloat("_Surface");
                    if (surface == 1)
                    {
                        mat.SetFloat("_Mode", 3); // Transparent
                        mat.SetOverrideTag("RenderType", "Transparent");
                        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                        mat.EnableKeyword("_ALPHABLEND_ON");
                    }
                }

                EditorUtility.SetDirty(mat);
                converted++;
            }
            else
            {
                skipped++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"✅ {converted} material(s) converted.\n⏭️ {skipped} material(s) skipped (not URP or already converted).");
    }
}
