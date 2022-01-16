using System;
using System.Collections.Generic;

namespace EditorWindows
{
    public class MapNode
    {
        public string Name = "New node";
        public string Description = "Empty description";
        public bool IsStarting = false;

        public string NorthNodeName = null;
        public string SouthNodeName = null;
        public string WestNodeName = null;
        public string EastNodeName = null;
        
        public MapNode NorthNode = null;
        public MapNode SouthNode = null;
        public MapNode WestNode = null;
        public MapNode EastNode = null;
    }
   
    public class Map
    {
        public string Name = "New Map";
        public List<MapNode> Nodes = new List<MapNode>();
    }
}