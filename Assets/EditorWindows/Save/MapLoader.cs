using System;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace EditorWindows.Save
{
    public static class MapLoader
    {
        public static SerializedMap LoadMap(string filePath)
        {
            try
            {
                if (filePath is null || filePath == "")
                {
                    Debug.LogError("Invalid file path");
                    return null;
                }

                var jsonData = File.ReadAllText(filePath);
                var newMap = JsonConvert.DeserializeObject<SerializedMap>(jsonData);
                if (newMap is null)
                {
                    Debug.LogError("Invalid map");
                    return null;
                }

                return newMap;
            }
            catch (Exception)
            {
                Debug.LogError("Impossible to load map");
                return null;
            }
        }
    }
}