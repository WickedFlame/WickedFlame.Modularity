using System.Collections.Generic;

namespace WickedFlame.Modularity
{
    public class ModuleCatalog
    {
        private readonly List<ModuleDescription> _moduleDescriptions;

        public ModuleCatalog()
        {
            _moduleDescriptions = new List<ModuleDescription>();
        }
        
        /// <summary>
        /// Gets the descriptions of the modules. This has to be a List for the XmlModuleCatalog to load the Modules 
        /// TODO: Change to IEnumerable
        /// </summary>
        public List<ModuleDescription> ModuleDescriptions
        {
            get
            {
                return _moduleDescriptions;
            }
        }

        public void AddDescription(ModuleDescription description)
        {
            _moduleDescriptions.Add(description);
        }
    }
}
