using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WickedFlame.Modularity.Composition;

namespace WickedFlame.Modularity
{
    public class ModuleLoader : IModuleLoader
    {
        public ModuleLoader()
        {
            _moduleCatalogs = new List<ModuleCatalog>();
        }

        private readonly IList<ModuleCatalog> _moduleCatalogs;
        public IEnumerable<ModuleCatalog> ModuleCatalogs
        {
            get
            {
                return _moduleCatalogs;
            }
        }

        public void AddModuleCatalog(ModuleCatalog catalog)
        {
            _moduleCatalogs.Add(catalog);
        }

        public void ComposeModules()
        {
            CreateModuleInstances();
        }

        public void ComposeModules(ModuleCatalog catalog)
        {
            AddModuleCatalog(catalog);
            CreateModuleInstances();
        }

        private void CreateModuleInstances()
        {
            var modules = new List<IModule>();
            var ignoreModules = new List<ModuleDescription>();

            foreach (var catalog in ModuleCatalogs)
            {
                // add all items in the modulecatolog in the correct order
                foreach (var item in catalog.ModuleDescriptions)
                {
                    if (item.IgnoreModule || ignoreModules.Any(m => m.TypeName == item.TypeName))
                    {
                        ignoreModules.Add(item);
                        continue;
                    }

                    if (modules.Any(m => m.GetType().Name == item.Type.Name))
                    {
                        // module is already instantiated
                        continue;
                    }

                    var module = InstanceFactory.CreateInstance(item.Type) as IModule;
                    var moduleType = module.GetType();

                    if (!string.IsNullOrEmpty(item.Version) && item.Version != moduleType.Assembly.GetName().Version.ToString())
                    {
                        //throw new ModuleVersionMismatchException(item.TypeName, item.Version, moduleType.Assembly.GetName().Version.ToString());
                        Trace.WriteLine(string.Format("Composed Module has a different version than the ModuleCatalog expected.\nModule {0} was loaded with versioin {1} while the ModuleCatalog expected version {2}", item.TypeName, item.Version, moduleType.Assembly.GetName().Version.ToString()));
                    }

                    if (item.Parameters.Count > 0)
                    {
                        // add parameters
                        var properties = module.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(ModuleParameterAttribute), false).Any())
                            .Select(p => new
                            {
                                Property = p,
                                Attribute = p.GetCustomAttributes(typeof(ModuleParameterAttribute), false).First() as ModuleParameterAttribute
                            });

                        foreach (var parameter in item.Parameters)
                        {
                            var property = properties.FirstOrDefault(p => p.Attribute.Key == parameter.Key);
                            if (property != null)
                            {
                                property.Property.SetValue(module, parameter.Value, null);
                            }
                        }
                    }


                    modules.Add(module);
                }
            }

            Modules = modules;
            ModulesLoaded = true;
        }

        public void InitializeModules()
        {
            // initialize all modules
            foreach (var module in Modules)
            {
                module.InitializeModule();
            }

            // call oninitialized after all modules are initialized
            foreach (var module in Modules)
            {
                module.OnInitialized();
            }
        }

        public void InitializeLoaded()
        {
            // call loaded on all modules
            foreach (var module in Modules)
            {
                module.Loaded();
            }
        }

        public IEnumerable<IModule> Modules { get; private set; }

        public bool ModulesLoaded { get; private set; }
    }
}
