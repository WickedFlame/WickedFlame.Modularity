using System.ComponentModel.Composition;
using WickedFlame.Modularity;

namespace WickedFlame.Modularity.UnitTest
{
    /// <summary>
    /// This class is only made to show how modules can be registered with attributes
    /// </summary>
    [ModuleMap(typeof(TestModule1), "test module 1")]
    [ModuleMap(typeof(TestModule2), "test module 2")]
    [Export(typeof(DummyModuleRegisterByAttribute))] // just to prove it does not matter...
    class DummyModuleRegisterByAttribute
    {
    }
}
