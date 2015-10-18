using System.Collections.Generic;

namespace WickedFlame.Modularity
{
    public interface IModuleLoader
    {
        void AddModuleCatalog(ModuleCatalog catalog);

        void ComposeModules();

        void InitializeModules();

        void InitializeLoaded();

        IEnumerable<IModule> Modules { get; }

        bool ModulesLoaded { get; }
    }
}
