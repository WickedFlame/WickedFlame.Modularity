using System.ComponentModel.Composition;
using WickedFlame.Modularity;

namespace WickedFlame.Modularity.UnitTest
{
    public class TestModule1 : IModule, ITestModule
    {
        public void InitializeModule()
        {
            IsInitialized = true;
        }

        public void OnInitialized()
        {
        }

        public void Loaded()
        {
        }

        public bool IsInitialized { get; set; }

        [ModuleParameter("Parameter1")]
        public bool BooleanParameter { get; set; }

        [ModuleParameter("Parameter2")]
        public string StringParameter { get; set; }
    }
}
