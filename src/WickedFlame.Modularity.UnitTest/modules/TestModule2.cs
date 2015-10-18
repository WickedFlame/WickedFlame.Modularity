using System.ComponentModel.Composition;
using WickedFlame.Modularity;

namespace WickedFlame.Modularity.UnitTest
{
    public class TestModule2 : IModule, ITestModule
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
    }
}
