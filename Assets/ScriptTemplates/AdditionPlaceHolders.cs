using UnityEditor;
using System.IO;

public class AdditionPlaceHolders : AssetModificationProcessor
{
    //If there would be more than one keyword to replace, add a Dictionary
    private const string ScriptNameSubAuthoring = "#SCRIPTNAME_SUB_AUTHORING#";

    public static void OnWillCreateAsset(string metaFilePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(metaFilePath);
        if (!fileName.EndsWith(".cs"))
            return;

        var actualFilePath = $"{Path.GetDirectoryName(metaFilePath)}{Path.DirectorySeparatorChar}{fileName}";
        var content = File.ReadAllText(actualFilePath);
        var newContent = content.Replace(ScriptNameSubAuthoring, fileName.Replace("Authoring.cs", ""));

        if (content == newContent) return;
        File.WriteAllText(actualFilePath, newContent);
        AssetDatabase.Refresh();
    }
}