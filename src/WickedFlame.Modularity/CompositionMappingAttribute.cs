using System;

namespace WickedFlame.Modularity
{
    /// <summary>
    /// Provides a attribute that can be placed on a <see cref="IModule"/> to map composeable parts that can be loaded from different assemblies without referencing each other
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ModuleMapAttribute : Attribute
    {
        public ModuleMapAttribute(Type type, string key)
        {
            Type = type;
            Key = key;
        }

        public Type Type { get; set; }

        public string Key { get; set; }
    }

    [Obsolete("Use ModuleMapAttribute instead", true)]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CompositionMappingAttribute : Attribute
    {
        public CompositionMappingAttribute(Type type, string key)
        {
            Type = type;
            Key = key;
        }

        public Type Type { get; set; }

        public string Key { get; set; }
    }
}
