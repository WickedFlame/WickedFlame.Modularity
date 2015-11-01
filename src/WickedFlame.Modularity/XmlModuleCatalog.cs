using System;
using System.Diagnostics;

namespace WickedFlame.Modularity
{
    public class XmlModuleCatalog : ModuleCatalog
    {
        private static object _lock = new object();

        public XmlModuleCatalog(string fileName)
        {
            var catalog = OpenCatalog(fileName);
            foreach (var description in catalog.ModuleDescriptions)
            {
                var type = Type.GetType(description.TypeName);
                if (type == null)
                {
                    Trace.WriteLine(string.Format("Type {0} could not be resolved", description.TypeName));
                    continue;
                }

                description.Type = type;

                AddDescription(description);
            }
        }

        private static ModuleCatalog OpenCatalog(string fileName)
        {
            lock(_lock)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return null;
                }

                var reader = new System.Xml.Serialization.XmlSerializer(typeof(ModuleCatalog));
                string filePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), fileName);
                using (var file = new System.IO.StreamReader(filePath))
                {
                    return (ModuleCatalog)reader.Deserialize(file);
                }
            }
        }
    }
}
