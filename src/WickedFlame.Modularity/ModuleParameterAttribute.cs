using System;

namespace WickedFlame.Modularity
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ModuleParameterAttribute : Attribute
    {
        public ModuleParameterAttribute(string key)
        {
            Key = key;
        }

        public string Key { get; set; }
    }
}
