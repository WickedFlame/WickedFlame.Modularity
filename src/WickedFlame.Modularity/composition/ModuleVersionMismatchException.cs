using System;

namespace WickedFlame.Modularity.Composition
{
    public class ModuleVersionMismatchException : Exception
    {
        public ModuleVersionMismatchException(string module, string expectedversion, string moduleversion)
            : base(string.Format("Composed Module has a different version than the ModuleCatalog expected.\nModule {0} was loaded with versioin {1} while the ModuleCatalog expected version {2}", module, moduleversion, expectedversion))
        {
            Module = module;
            ExpectedVersion = expectedversion;
            ModuleVersion = moduleversion;
        }

        public string Module { get; private set; }

        public string ExpectedVersion { get; private set; }

        public string ModuleVersion { get; private set; }
    }
}
