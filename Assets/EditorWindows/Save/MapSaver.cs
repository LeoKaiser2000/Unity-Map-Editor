using System;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace EditorWindows.Save
{
    public static class MapSaver
    {
        public static void SaveMap(SerializedMap map)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(map, Formatting.Indented);
                var filePath = EditorUtility.SaveFilePanel("Save map file", "", map.name, "json");
                if (filePath is null or "")
                {
                    Debug.LogError("Invalid file path");
                    return;
                }
                System.IO.File.WriteAllText(filePath, jsonData);
            }
            catch (Exception)
            {
                Debug.LogError("Impossible to save map");
            }
        }
    }
}