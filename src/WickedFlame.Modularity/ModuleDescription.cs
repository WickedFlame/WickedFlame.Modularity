using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WickedFlame.Modularity
{
    public class ModuleDescription
    {
        public ModuleDescription()
        {
            Parameters = new List<Parameter>();
        }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute("Assembly")]
        public string AssemblyName { get; set; }

        [XmlAttribute("Type")]
        public string TypeName { get; set; }

        [XmlIgnore]
        public Type Type { get; set; }

        [XmlAttribute]
        public string Version { get; set; }

        [XmlArray]
        public List<Parameter> Parameters { get; set; }

        [XmlAttribute]
        public bool IgnoreModule { get; set; }
    }
}
