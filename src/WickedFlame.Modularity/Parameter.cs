using System.Xml.Serialization;

namespace WickedFlame.Modularity
{
    //[XmlRoot("Parameter")]
    [XmlInclude(typeof(string))]
    [XmlInclude(typeof(int))]
    [XmlInclude(typeof(double))]
    [XmlInclude(typeof(bool))]
    public class Parameter
    {
        [XmlAttribute]
        public string Key { get; set; }

        //[XmlAttribute]
        public object Value { get; set; }
    }
}
