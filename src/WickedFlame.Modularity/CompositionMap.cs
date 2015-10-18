using System;
using System.Reflection;

namespace WickedFlame.Modularity
{
    /// <summary>
    /// Provides information for the compositionmapping. With this map a component can be composed from different a assembly without them referencing each other
    /// </summary>
    public class CompositionMap
    {
        public CompositionMap(string key, Type type)
        {
            Assembly = type.Assembly;
            ComposingType = type;
            Key = key;
        }

        public Assembly Assembly { get; private set; }

        public Type ComposingType { get; private set; }

        public string Key { get; private set; }
    }
}
