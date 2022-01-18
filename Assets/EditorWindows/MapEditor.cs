using System.Collections.Generic;
using EditorWindows.Save;
using UnityEngine;
using UnityEditor;

namespace EditorWindows
{
    public class MapEditor : EditorWindow
    {
        private Map _map;
        private MapNode _selectedNode;
        
        [MenuItem("Map/Editor")]
        public static void  ShowWindow () {
            GetWindow(typeof(MapEditor));
        }
        
        private void OnEnable()
        {
            SetMap(new Map());
        }

        public void OnGUI()
        {
            ShowMapLoading();
            if (_map == null) return;
            ShowCurrentMap();
            if (_map == null) return;
            ShowCurrentMapProperties();
        }

        private void SetMap(Map map)
        {
            _map = map;
            _selectedNode = null;
        }

        private void SaveMap()
        {
            var serializedMap = MapSerializer.SerializeMap(_map);
            if (serializedMap != null)
                MapSaver.SaveMap(serializedMap);
        }

        private void LoadMap()
        {
            var filePath = EditorUtility.OpenFilePanel("Open map file", "", "json");
            if (filePath is null || filePath == "")
                return;
            var serializedMap = MapLoader.LoadMap(filePath);
            if (serializedMap == null) return;
            var newMap = MapSerializer.UnSerializeMap(serializedMap);
            if (newMap != null)
                SetMap(newMap);
        }

        private void ShowMapLoading()
        {
            GUILayout.Label("Map loading", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load"))
                LoadMap();

            if (GUILayout.Button("New map"))
                SetMap(new Map());
            EditorGUILayout.EndHorizontal();
        }

        private void ShowCurrentMap()
        {
            GUILayout.Label("Current map", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
                SaveMap();
            
            if (GUILayout.Button("Close"))
                SetMap(null);
            EditorGUILayout.EndHorizontal();
        }

        private void ShowCurrentMapProperties()
        {
            GUILayout.Label("Current map properties", EditorStyles.boldLabel);
            _map.Name = EditorGUILayout.TextField("Name", _map.Name);
            
            ShowMapNodes();
            
            if (GUILayout.Button("Add a node"))
                _map.Nodes.Add(new MapNode());
        }

        private void ShowMapNodes()
        {
            var toRemoveNodeList = new List<MapNode>();
            if (int.TryParse(EditorGUILayout.TextField("Nodes", _map.Nodes.Count.ToString(), EditorStyles.numberField),
                    out var number))
            {
                if (number >= 0)
                {
                    if (number > _map.Nodes.Count)
                    {
                        while (_map.Nodes.Count < number)
                             _map.Nodes.Add(new MapNode());
                    } else if (number < _map.Nodes.Count)
                    {
                        for (var i = number; i < _map.Nodes.Count; ++i)
                            toRemoveNodeList.Add(_map.Nodes[i]);
                    }
                }
            }

            foreach (var node in _map.Nodes)
            {
                GUILayout.Label($"Node {node.Name}", EditorStyles.boldLabel);
                node.Name = EditorGUILayout.TextField("Name", node.Name);
                node.Description = EditorGUILayout.TextField("Description", node.Description);
                node.IsStarting = EditorGUILayout.Toggle("Is starting", node.IsStarting);
                
                ShowNorthNode(node);
                ShowSouthNode(node);
                ShowWestNode(node);
                ShowEastNode(node);
                
                EditorGUILayout.BeginHorizontal();
                if (node == _selectedNode)
                {
                    if (GUILayout.Button("Unselect node"))
                        _selectedNode = null;
                } else if (GUILayout.Button("Select node"))
                    _selectedNode = node;
                
                if (GUILayout.Button("Remove node"))
                    toRemoveNodeList.Add(node);
                EditorGUILayout.EndHorizontal();
            }
            RemoveNodes(toRemoveNodeList);
        }

        private void RemoveNodes(ICollection<MapNode> toRemoveNodeList)
        {
            foreach (var node in _map.Nodes)
            {
                if (node.NorthNode != null && toRemoveNodeList.Contains(node.NorthNode))
                    node.NorthNode = null;
                if (node.SouthNode != null && toRemoveNodeList.Contains(node.SouthNode))
                    node.SouthNode = null;
                if (node.WestNode != null && toRemoveNodeList.Contains(node.WestNode))
                    node.WestNode = null;
                if (node.EastNode != null && toRemoveNodeList.Contains(node.EastNode))
                    node.EastNode = null;
            }

            if (_selectedNode != null && toRemoveNodeList.Contains(_selectedNode))
                _selectedNode = null;
            _map.Nodes.RemoveAll(toRemoveNodeList.Contains);
        }

        private void ShowNorthNode(MapNode node)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("North node", node.NorthNode is null ? "No" : node.NorthNode.Name);
            if (_selectedNode != null && _selectedNode != node && _selectedNode != node.NorthNode)
            {
                if (GUILayout.Button($"Link {_selectedNode.Name}"))
                    node.NorthNode = _selectedNode;
            }
            if (node.NorthNode != null)
            {
                if (GUILayout.Button($"Remove link"))
                    node.NorthNode = null;
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void ShowSouthNode(MapNode node)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("South node", node.SouthNode is null ? "No" : node.SouthNode.Name);
            if (_selectedNode != null && _selectedNode != node && _selectedNode != node.SouthNode)
            {
                if (GUILayout.Button($"Link {_selectedNode.Name}"))
                    node.SouthNode = _selectedNode;
            }
            if (node.SouthNode != null)
            {
                if (GUILayout.Button($"Remove link"))
                    node.SouthNode = null;
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void ShowWestNode(MapNode node)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("West node", node.WestNode is null ? "No" : node.WestNode.Name);
            if (_selectedNode != null && _selectedNode != node && _selectedNode != node.WestNode)
            {
                if (GUILayout.Button($"Link {_selectedNode.Name}"))
                    node.WestNode = _selectedNode;
            }
            if (node.WestNode != null)
            {
                if (GUILayout.Button($"Remove link"))
                    node.WestNode = null;
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void ShowEastNode(MapNode node)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("East node", node.EastNode is null ? "No" : node.EastNode.Name);
            if (_selectedNode != null && _selectedNode != node && _selectedNode != node.EastNode)
            {
                if (GUILayout.Button($"Link {_selectedNode.Name}"))
                    node.EastNode = _selectedNode;
            }
            if (node.EastNode != null)
            {
                if (GUILayout.Button($"Remove link"))
                    node.EastNode = null;
            }
            EditorGUILayout.EndHorizontal();
        }

    }
}
