using System;
using System.Diagnostics;
using EditorWindows.Save;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

/*
 * Elapsed time with my non competitive laptop
 * For a map with 3 elements in
 * 100 times -> 2.12 s
 * 1000 times -> 13.53 s
 * 10000 times -> 1 min 45.64 s
 */

/*
 * - Is it acceptable to use reflection in runtime of the games?
 * Reflexion can be usefull for extensibility, debugging and testing tools
 * but the performences cost is to expensive for a game runtime
 * - Would you use it at runtime?
 * With this kind of usage ; never!
 * - Why would / would’t you use it in runtime?
 * The performence overall is to expensive for a game.
 * Randering, inputs reading and gameplay loop would loose to much.
 * But I could imagine using it at program initialization phase, or for code generation,
 * with, for exemple, usage of attributes.
 */


namespace EditorWindows
{
    public class MapInspector : EditorWindow
    {
        private object _map;
        private int _logNumber;

        private enum DisplayMode
        {
            GUILayout = 0,
            DebugLog = 1
        }
            
        [MenuItem("Map/Inspector")]
        public static void  ShowWindow () {
            GetWindow(typeof(MapInspector));
        }
        
        private void OnEnable()
        {
            _map = null;
            _logNumber = 10000;
        }

        public void OnGUI()
        {
            GUILayout.Label("Map loading", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load"))
                LoadMap();
            if (GUILayout.Button($"Log {_logNumber} time{(_logNumber > 1 ? "s" : "")}"))
                LoadAndLoadMapXTimes(_logNumber);
            EditorGUILayout.EndHorizontal();
            _logNumber = EditorGUILayout.IntField($"Load Log time{(_logNumber > 1 ? "s" : "")}", _logNumber);
            if (_map is null)
                return;
            try
            {
                GUILayout.Label("Map", EditorStyles.boldLabel);
                DisplayFieldsOff(_map, DisplayMode.GUILayout);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void DisplayFieldsOff(object obj, DisplayMode displayMode)
        {
            foreach (var propertyInfo in obj.GetType().GetFields())
            {
                var value = propertyInfo.GetValue(obj);
                if (value is null)
                {
                    if (displayMode == DisplayMode.GUILayout)
                        EditorGUILayout.LabelField($"{propertyInfo.Name}", $"no {propertyInfo.Name}");
                    else if (displayMode == DisplayMode.DebugLog)
                        Debug.Log($"{propertyInfo.Name}: no {propertyInfo.Name}");
                }
                else if (propertyInfo.FieldType.IsArray)
                {
                    var childObjTab = (object [])value;
                    if (displayMode == DisplayMode.GUILayout)
                        EditorGUILayout.LabelField($"{propertyInfo.Name} array", $"{childObjTab.Length} element{(childObjTab.Length > 1 ? "s" : "")}");
                    else if (displayMode == DisplayMode.DebugLog)
                        Debug.Log($"{propertyInfo.Name} array: {childObjTab.Length} element{(childObjTab.Length > 1 ? "s" : "")}");
                    for (var i = 0; i < childObjTab.Length; ++i)
                    {
                        if (displayMode == DisplayMode.GUILayout)
                            GUILayout.Label($"Elem[{i}]:");
                        else if (displayMode == DisplayMode.DebugLog)
                            Debug.Log($"Elem[{i}]:");
                        DisplayFieldsOff(childObjTab[i], displayMode);
                    }
                }
                else
                {
                    if (displayMode == DisplayMode.GUILayout)
                        EditorGUILayout.LabelField($"{propertyInfo.Name}", $"{value}");
                    else if (displayMode == DisplayMode.DebugLog)
                        Debug.Log($"{propertyInfo.Name}: {value}");
                }
            }
        }

        private void LoadMap()
        {
            var filePath = EditorUtility.OpenFilePanel("Open map file", "", "json");
            if (filePath is null || filePath == "")
                return;
            var serializedMap = MapLoader.LoadMap(filePath);
            if (serializedMap == null) return;
            _map = serializedMap;
        }

        private static void LoadAndLoadMapXTimes(int times)
        {
            var filePath = EditorUtility.OpenFilePanel("Open map file", "", "json");
            if (filePath is null || filePath == "")
                return;
            Debug.Log("Starting stopwatch...");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (var i = 0; i < times; ++i)
                LoadAndLoadMap(filePath);
            stopWatch.Stop();
            Debug.Log("Ending stopwatch...");
            var ts = stopWatch.Elapsed;

            var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Debug.Log("Elapsed time:" + elapsedTime);
        }

        private static void LoadAndLoadMap(string filePath)
        {
            var serializedMap = MapLoader.LoadMap(filePath);
            if (serializedMap is null)
                return;
            DisplayFieldsOff(serializedMap, DisplayMode.DebugLog);
        }
    }
}