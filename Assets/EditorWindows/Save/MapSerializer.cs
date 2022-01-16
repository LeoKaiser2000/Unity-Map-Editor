using System.Linq;
using UnityEngine;

namespace EditorWindows.Save
{
    public static class MapSerializer
    {
        public static SerializedMap SerializeMap(Map map)
        {
            if (map.Nodes.GroupBy(x => x.Name).Any(g => g.Count() != 1))
            {
                Debug.LogError($"Error: Some node have the same name in map {map.Name}");
                return null;
            }
            return new SerializedMap
            {
                name = map.Name,
                nodes = map.Nodes.Select(node => new SerializedMapNode()
                {
                    name = node.Name,
                    description =  node.Description,
                    isStarting = node.IsStarting,
                    northNode = node.NorthNode?.Name,
                    southNode = node.SouthNode?.Name,
                    westNode = node.WestNode?.Name,
                    eastNode = node.EastNode?.Name,

                }).ToArray()
            };
        }
        
        public static Map UnSerializeMap(SerializedMap serializedMap)
        {
            if (serializedMap.nodes.GroupBy(x => x.name).Any(g => g.Count() != 1))
            {
                Debug.LogError($"Error: Some node have the same name in map {serializedMap.name}");
                return null;
            }

            var lol = new Map
            {
                Name = serializedMap.name,
                Nodes = serializedMap.nodes.Select(node => new MapNode()
                {
                    Name = node.name,
                    Description =  node.description,
                    IsStarting = node.isStarting,
                    NorthNode = null,
                    SouthNode = null,
                    WestNode = null,
                    EastNode = null,

                }).ToList()
            };

            for (var i = 0; i < lol.Nodes.Count; ++i)
            {
                var northNodeName = serializedMap.nodes[i].northNode;
                var southNodeName = serializedMap.nodes[i].southNode;
                var westNodeName = serializedMap.nodes[i].westNode;
                var eastNodeName = serializedMap.nodes[i].eastNode;

                if (northNodeName != null)
                {
                    var node = GetMapNode(lol, lol.Nodes[i], northNodeName);
                    if (node is null)
                        return null;
                    lol.Nodes[i].NorthNode = node;
                }
                if (southNodeName != null)
                {
                    var node = GetMapNode(lol, lol.Nodes[i], southNodeName);
                    if (node is null)
                        return null;
                    lol.Nodes[i].SouthNode = node;
                }
                if (westNodeName != null)
                {
                    var node = GetMapNode(lol, lol.Nodes[i], westNodeName);
                    if (node is null)
                        return null;
                    lol.Nodes[i].WestNode = node;
                }
                if (eastNodeName != null)
                {
                    var node = GetMapNode(lol, lol.Nodes[i], eastNodeName);
                    if (node is null)
                        return null;
                    lol.Nodes[i].EastNode = node;
                }
            }
            return lol;
        }

        private static MapNode GetMapNode(Map map, MapNode currentNode, string linkedNodeName)
        {
            var node = map.Nodes.FirstOrDefault(mapNode => mapNode.Name.Equals(linkedNodeName));
            if (node is null)
            {
                Debug.LogError($"Error: Node {linkedNodeName} could not be found in map {map.Name}");
                return null;
            }
            if (node == currentNode)
            {
                Debug.LogError($"Error: Node {linkedNodeName} is link with itself in map {map.Name}");
                return null;
            }

            return node;
        }
    }
}