using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text;

public class PrefabEnumGenerator
{
    [MenuItem("Tools/Generate PrefabType Enum")]
    public static void GeneratePrefabEnum()
    {
        string[] prefabPaths = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Resources/Prefabs" })
            .Select(AssetDatabase.GUIDToAssetPath)
            .ToArray();

        StringBuilder enumBuilder = new StringBuilder();
        enumBuilder.AppendLine("public enum PrefabType");
        enumBuilder.AppendLine("{");

        for (int i = 0; i < prefabPaths.Length; i++)
        {
            string prefabName = Path.GetFileNameWithoutExtension(prefabPaths[i]);
            enumBuilder.AppendLine($"    {prefabName} = {i},");
        }

        enumBuilder.AppendLine("}");

        string enumFilePath = "Assets/Scripts/Utils/PrefabType.cs";
        File.WriteAllText(enumFilePath, enumBuilder.ToString());
        AssetDatabase.Refresh();

        Debug.Log("PrefabType enum generated successfully!");
    }
}