using System;
using System.Linq;
using System.Reflection;

namespace WickedFlame.Modularity
{
    /// <summary>
    /// Load all ModuleMapAttributes from the Assembly and create ModuleDescriptions based on the Attributes
    /// </summary>
    public class AssemblyModuleCatalog : ModuleCatalog
    {
        /// <summary>
        /// Load all ModuleMapAttributes from the Assembly and create ModuleDescriptions based on the Attributes
        /// </summary>
        /// <param name="assembly">The Assembly to check for ModuleMapAttributes</param>
        public AssemblyModuleCatalog(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(ModuleMapAttribute))))
            {
                var attributes = type.GetCustomAttributes(typeof(ModuleMapAttribute));
                foreach (ModuleMapAttribute attribute in attributes)
                {
                    AddDescription(new ModuleDescription
                    {
                        AssemblyName = assembly.GetName().Name,
                        Name = attribute.Key,
                        //Parameters
                        TypeName = attribute.Type.Name,// type.Name,
                        Type = attribute.Type,// type,
                        Version = assembly.GetName().Version.ToString()
                        //Version
                    });
                }
            }
        }
    }
}
