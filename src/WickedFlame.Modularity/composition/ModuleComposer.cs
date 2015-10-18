using System;
using System.Collections.Generic;

namespace WickedFlame.Modularity.Composition
{
    class ModuleComposer
    {
        #region Composition

        public IEnumerable<IModule> Imports { get; private set; }

        public void Compose()
        {
            //var composer = new CompositionContainer<IModule>();
            //composer.Catalogs.Add(new ComposeableCatalog(".", "*.dll"));
            //composer.Catalogs.Add(new ComposeableCatalog(".", "*.exe"));

            //// fill the imports of this object
            //try
            //{
            //    composer.Compose();
            //    Imports = composer.ComposedParts;
            //}
            //catch (Exception e)
            //{
            //    System.Diagnostics.Trace.WriteLine(e);
            //}
        }


        #endregion
    }
}
