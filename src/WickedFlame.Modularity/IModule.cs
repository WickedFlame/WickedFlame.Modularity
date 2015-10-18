
namespace WickedFlame.Modularity
{
    public interface IModule
    {
        void InitializeModule();

        void OnInitialized();

        void Loaded();
    }
}
