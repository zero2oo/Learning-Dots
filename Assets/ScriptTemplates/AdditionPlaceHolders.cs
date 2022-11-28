using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class AdditionPlaceHolders : AssetModificationProcessor
{
    ///Example:
    //String:  <HarryPotter replace_Potter_Kane lower_0>
    //Result:   harryKane
    private const string Replace = "replace_"; 
    private const string LowerCase = "lower_";
    private const string Sub = "sub_";

    public static void OnWillCreateAsset(string metaFilePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(metaFilePath);
        if (!fileName.EndsWith(".cs"))
            return;

        var actualFilePath = $"{Path.GetDirectoryName(metaFilePath)}{Path.DirectorySeparatorChar}{fileName}";
        var content = File.ReadAllText(actualFilePath);
        
        var matches = Regex.Matches(content, "|(.+?)|");
        if (matches.Count == 0) return;
        
        foreach (Match match in matches)
        {
            var listStr = match.Value.Trim('<', '>').Split(' ');
            if (listStr.Length < 2)
            {
                Debug.LogError("Wrong format!");
                continue;
            }
            
            for (var i = 1; i < listStr.Length; i++)
            {
                Modify(ref listStr[0], listStr[i]);
            }

            content = content.Replace(match.Value, listStr[0]);
        }
        
        File.WriteAllText(actualFilePath, content);
        AssetDatabase.Refresh();
    }
    
    private static void Modify(ref string content, string expression)
    {
        if (expression.Contains(Replace))
        {
            var listStr = expression.Split("_");
            if (listStr.Length != 3)
            {
                Debug.LogError("Wrong format!");
                return;
            }
            
            content = content.Replace(listStr[1], listStr[2]);
            return;
        }
        
        if (expression.Contains(LowerCase))
        {
            var listStr = expression.Split("_");
            if (listStr.Length != 2)  {
                Debug.LogError("Wrong format!");
                return;
            };

            if (int.TryParse(listStr[1], out var number))
            {
                var lowerChar = char.ToLower(content[number]);
                content = content.Replace(content[number], lowerChar);
            }
            else
            {
                Debug.LogError("Following 'lower_' must be a number!");
            }
            // return;
        }

        // if (expression.Contains(Sub))
        // {
        //     var listStr = expression.Split("_");
        //     if (listStr.Length != 2)
        //     {
        //         Debug.LogError("Wrong format!");
        //         return;
        //     }
        //     
        //     content = content.Replace(listStr[1], listStr[2]);
        // }

    }
}