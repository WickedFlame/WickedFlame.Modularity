using System.Collections.Generic;

namespace WickedFlame.Modularity
{
    public interface ICompositionMapper
    {
        IDictionary<string, CompositionMap> CompositionMaps { get; }

        IList<IModule> Modules { get; }
    }

    /// <summary>
    /// Provides functuality to map Composeable parts with a key to a IModule so that they can be loaded from another assembly without referencing eachother
    /// </summary>
    public class CompositionMapper : ICompositionMapper
    {
        public CompositionMapper(IModuleLoader loader)
        {
            if(!loader.ModulesLoaded)
                loader.InitializeModules();

            Modules = new List<IModule>();
            CompositionMaps = new Dictionary<string, CompositionMap>();

            foreach (var module in loader.Modules)
            {
                Modules.Add(module);

                foreach (ModuleMapAttribute mapping in module.GetType().GetCustomAttributes(typeof(ModuleMapAttribute), false))
                {
                    CompositionMaps.Add(mapping.Key, new CompositionMap(mapping.Key, mapping.Type));
                }
            }
        }

        public IList<IModule> Modules { get; private set; }

        public IDictionary<string, CompositionMap> CompositionMaps { get; private set; }
    }
}
