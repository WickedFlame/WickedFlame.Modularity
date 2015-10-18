using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WickedFlame.Modularity.UnitTest;
using System.Linq;
using WickedFlame.Modularity;
using WickedFlame.Modularity.Composition;

namespace wickedflame.modularity.test
{
    [TestClass]
    public class ModuleLoaderTest
    {
        [TestMethod]
        public void LoadModuleCatalogTest()
        {
            var catalog = new ModuleCatalog();
            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module2",
                AssemblyName = typeof(TestModule2).Assembly.GetName().Name,
                Type = typeof(TestModule2),
                Version = "1.0.0.0"
            });

            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module1",
                AssemblyName = typeof(TestModule1).Assembly.GetName().Name,
                Type = typeof(TestModule1),
                Version = "1.0.0.0"
            });

            Assert.IsNotNull(catalog);
            Assert.IsTrue(catalog.ModuleDescriptions.Count() == 2);
            Assert.AreEqual(catalog.ModuleDescriptions.First().Type, typeof(TestModule2));
            Assert.AreEqual(catalog.ModuleDescriptions.Last().Type, typeof(TestModule1));
        }

        [TestMethod]
        public void LoadModulesOrderedTest()
        {
            var catalog = new ModuleCatalog();
            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module1",
                AssemblyName = typeof(TestModule1).Assembly.GetName().Name,
                Type = typeof(TestModule1),
                Version = "1.0.0.0"
            });

            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module2",
                AssemblyName = typeof(TestModule2).Assembly.GetName().Name,
                Type = typeof(TestModule2),
                Version = "1.0.0.0"
            });

            //tmp.SaveCatalog("ModuleCatalog.xml");

            var loader = new ModuleLoader();
            loader.ComposeModules(catalog);

            Assert.IsTrue(loader.Modules.Count() == 2);
            Assert.AreEqual(loader.Modules.First().GetType(), typeof(TestModule1));
            Assert.AreEqual(loader.Modules.Last().GetType(), typeof(TestModule2));
        }

        //[TestMethod]
        //public void LoadModulesNotOrderedTest()
        //{
        //    var loader = new ModuleLoader();
        //    loader.ComposeModules();

        //    Assert.IsTrue(loader.Modules.Count() == 2);
        //    Assert.AreEqual(loader.Modules.First().GetType(), typeof(TestModule2));
        //    Assert.AreEqual(loader.Modules.Last().GetType(), typeof(TestModule1));
        //}

        [TestMethod]
        //[ExpectedException(typeof(ModuleVersionMismatchException))]
        public void LoadModulesWithVersionCheck()
        {
            var catalog = new ModuleCatalog();
            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module1",
                AssemblyName = typeof(TestModule1).Assembly.GetName().Name,
                Type = typeof(TestModule1),
                Version = "2.0.0.0"
            });

            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module2",
                AssemblyName = typeof(TestModule2).Assembly.GetName().Name,
                Type = typeof(TestModule2),
                Version = "1.0.0.0"
            });

            var loader = new ModuleLoader();

            // ModuleVersionMismatchException has to be thrown because of the wrong version
            loader.ComposeModules(catalog);
        }

        [TestMethod]
        public void LoadModulesWithoutVersionCheck()
        {
            var catalog = new ModuleCatalog();
            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module1",
                AssemblyName = typeof(TestModule1).Assembly.GetName().Name,
                Type = typeof(TestModule1),
            });
            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module2",
                AssemblyName = typeof(TestModule2).Assembly.GetName().Name,
                Type = typeof(TestModule2),
            });
            
            var loader = new ModuleLoader();

            // ModuleVersionMismatchException is not allowed to be thrown because no version has been defined in the catalog
            loader.ComposeModules(catalog);

            Assert.IsTrue(loader.Modules.Count() == 2);
        }

        [TestMethod]
        public void InitializeModulesTest()
        {
            var catalog = new ModuleCatalog();
            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module1",
                AssemblyName = typeof(TestModule1).Assembly.GetName().Name,
                Type = typeof(TestModule1),
                Version = "1.0.0.0"
            });

            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module2",
                AssemblyName = typeof(TestModule2).Assembly.GetName().Name,
                Type = typeof(TestModule2),
                Version = "1.0.0.0"
            });

            //tmp.SaveCatalog("ModuleCatalog.xml");

            var loader = new ModuleLoader();
            loader.ComposeModules(catalog);

            Assert.IsTrue(loader.Modules.Count() == 2);

            foreach (var module in loader.Modules)
                Assert.IsFalse(((ITestModule)module).IsInitialized);

            loader.InitializeModules();

            foreach (var module in loader.Modules)
                Assert.IsTrue(((ITestModule)module).IsInitialized);
        }
        
        [TestMethod]
        [Description("Loads a module and adds some parameters")]
        public void LoadModuleWithParameters()
        {
            var catalog = new ModuleCatalog();
            catalog.AddDescription(new ModuleDescription
            {
                Name = "Module1",
                AssemblyName = typeof(TestModule1).Assembly.GetName().Name,
                Type = typeof(TestModule1),
                Parameters = new List<Parameter>
                        {
                            new Parameter
                            {
                                Key = "Parameter1",
                                Value = true
                            },
                            new Parameter
                            {
                                Key = "Parameter2",
                                Value = "test"
                            }
                        }
            });

            //tmp.SaveCatalog("ModuleCatalog.xml");

            var loader = new ModuleLoader();

            loader.ComposeModules(catalog);

            var module = loader.Modules.First();
            Assert.IsTrue(((TestModule1)module).BooleanParameter);
            Assert.IsTrue(((TestModule1)module).StringParameter == "test");
        }

        [TestMethod]
        public void LoadModulesWithAssemblyModuleCatalog()
        {
            var catalog = new AssemblyModuleCatalog(GetType().Assembly);
            var loader = new ModuleLoader();
            loader.AddModuleCatalog(catalog);

            loader.ComposeModules();

            Assert.IsTrue(loader.Modules.Count() == 2);
        }

        [TestMethod]
        public void LoadModulesWithAssemblyModuleCatalog2()
        {
            var catalog = new AssemblyModuleCatalog(GetType().Assembly);
            var loader = new ModuleLoader();

            loader.ComposeModules(catalog);

            Assert.IsTrue(loader.Modules.Count() == 2);
        }
    }
}
