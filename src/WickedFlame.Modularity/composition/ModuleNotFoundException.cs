using System;

namespace WickedFlame.Modularity.Composition
{
    public class ModuleNotFoundException : Exception
    {
        public ModuleNotFoundException(string module, string version)
            : base(string.Format("Module {0}, version {1}, from the ModuleCatalog was not found or could not be loaded", module, version))
        {
            Module = module;
            Version = version;
        }

        public string Module { get; private set; }

		public string Version { get; private set; }
    }
}
