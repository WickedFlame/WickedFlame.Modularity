using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WickedFlame.Modularity.UnitTest
{
    [TestClass]
    public class XmlModuleLoaderTests
    {
        [TestMethod]
        public void LoadModulesFromXml_Simple()
        {
            var catalog = new XmlModuleCatalog("SimpleModuleCatalog.xml");
            var loader = new ModuleLoader();
            loader.AddModuleCatalog(catalog);
            loader.ComposeModules();

            Assert.IsTrue(loader.Modules.Count() == 2);
        }

        [TestMethod]
        public void LoadModulesFromXml_WithIgnore()
        {
            var catalog = new XmlModuleCatalog("IgnoreModuleCatalog.xml");
            var loader = new ModuleLoader();
            loader.AddModuleCatalog(catalog);
            loader.ComposeModules();

            Assert.IsTrue(loader.Modules.Count() == 1);
            Assert.IsTrue(loader.Modules.First().GetType() == typeof(TestModule2));
        }

        [TestMethod]
        public void LoadModulesFromXml_WithParameters()
        {
            var catalog = new XmlModuleCatalog("ParameterizedModuleCatalog.xml");
            var loader = new ModuleLoader();
            loader.AddModuleCatalog(catalog);
            loader.ComposeModules();

            Assert.IsTrue(loader.Modules.Count() == 1);
            Assert.IsTrue(loader.Modules.First().GetType() == typeof(TestModule1));

            var module = loader.Modules.First() as TestModule1;
            Assert.IsTrue(module.BooleanParameter == true);
            Assert.IsTrue(module.StringParameter == "Test value");
        }
    }
}
