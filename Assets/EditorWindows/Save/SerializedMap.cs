using System;

namespace EditorWindows.Save
{
    [Serializable]
    public class SerializedMapNode
    {
        public string name;
        public string description;
        public bool isStarting;

        public string northNode;
        public string southNode;
        public string westNode;
        public string eastNode;
    }

    [Serializable]
    public class SerializedMap
    {
        public string name;
        public SerializedMapNode[] nodes;
    }
}