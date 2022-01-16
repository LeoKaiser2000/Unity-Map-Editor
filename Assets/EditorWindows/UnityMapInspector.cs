using EditorWindows.Save;
using UnityEngine;
using UnityEditor;

namespace EditorWindows
{
    public class UnityMapInspector : EditorWindow
    {
        public SerializedMap map;
        private SerializedObject _so;
        private SerializedProperty _mapProperty;

        [MenuItem("Map/UnityInspector")]
        public static void  ShowWindow () {
            EditorWindow.GetWindow(typeof(UnityMapInspector));
        }
        
        private void OnEnable()
        {
            SetMap(new SerializedMap());
        }

        public void OnGUI()
        {
            GUILayout.Label("Map loading", EditorStyles.boldLabel);
            if (GUILayout.Button("Load"))
                LoadMap();

            if (GUILayout.Button("New map"))
                SetMap(new SerializedMap());
            
            if (map is null)
                return;
            GUILayout.Label("Map saving", EditorStyles.boldLabel);
            if (GUILayout.Button("Save"))
                SaveMap();

            GUILayout.Label("Map properties", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_mapProperty, true);
            _so.ApplyModifiedProperties();
        }
        
        private void SetMap(SerializedMap newMap)
        {
            this.map = newMap;
            _so = new SerializedObject(this);
            _mapProperty = _so.FindProperty("Map");
        }
        
        private void SaveMap()
        {
            if (map != null)
                MapSaver.SaveMap(map);
        }

        private void LoadMap()
        {
            var filePath = EditorUtility.OpenFilePanel("Open map file", "", "json");
            if (filePath is null or "")
                return;
            var serializedMap = MapLoader.LoadMap(filePath);
            if (serializedMap == null) return;
            SetMap(serializedMap);
        }
    }
}
