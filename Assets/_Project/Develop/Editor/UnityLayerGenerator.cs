using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Assets._Project.Develop.Editor
{
	public class UnityLayerGenerator
	{
		private static string OutputPath
			=> Path.Combine(Application.dataPath, "_Project/Develop/Runtime/Utilities/UnityLayers.cs");

		[InitializeOnLoadMethod]
		[MenuItem("Tools/GenerateUnityLayers")]
		private static void Generate()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine($"public static class UnityLayers");
			sb.AppendLine("{");


			// Генерация свойств для каждого слоя
			//for (int i = 0; i < 32; i++)
			//{
			//	string fullComponentName = typeof(LayerMask).FullName;
			//	string componentName = typeof(LayerMask).Name;
			//	string layerName = LayerMask.LayerToName(i);

			//	if (!string.IsNullOrEmpty(layerName))
			//	{
			//		sb.AppendLine($"\tpublic static readonly int Layer{GetLayerName(layerName)} = {fullComponentName}.NameToLayer(\"{layerName}\");");
			//		sb.AppendLine($"\tpublic static readonly int {componentName}{GetLayerName(layerName)} = 1 << Layer{GetLayerName(layerName)};");
			//		sb.AppendLine();
			//	}
			//}

			string[] layerNames = InternalEditorUtility.layers;

			string fullComponentName = typeof(LayerMask).FullName;
			string componentName = typeof(LayerMask).Name;

			foreach (string layerName in layerNames)
			{
				sb.AppendLine($"\tpublic static readonly int Layer{GetLayerName(layerName)} = {fullComponentName}.NameToLayer(\"{layerName}\");");
				sb.AppendLine($"\tpublic static readonly int {componentName}{GetLayerName(layerName)} = 1 << Layer{GetLayerName(layerName)};");
				sb.AppendLine();
			}

			sb.AppendLine("}");

			File.WriteAllText(OutputPath, sb.ToString());

			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
		}

		private static string GetLayerName(string layerName) => layerName.Replace(" ", "");		
	}
}
