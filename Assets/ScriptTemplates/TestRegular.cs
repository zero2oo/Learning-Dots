using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

public class TestRegular : MonoBehaviour
{
    public string testStr;

    ///Example:
    //String:  <HarryPotter replace_Potter_Kane lower_0>
    //Result:   harryKane
    private const string Replace = "replace_"; 
    private const string LowerCase = "lower_";
    private const string Sub = "sub_";
    
    public void Test()
    {
        var content = testStr;
        var matches = Regex.Matches(content, "<(.+?)>");

        if (matches.Count == 0)
        {
            Debug.LogError("not matched");
            return;
        }
        
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
            Debug.Log(content);
        }
    }

    private void Modify(ref string content, string expression)
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
