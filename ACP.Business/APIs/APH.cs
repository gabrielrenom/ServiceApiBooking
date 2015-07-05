using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ACP.Business.APIs
{
    public class APH
    {
        public APH()
        { }

        private string Serialize<T>(T objecttobeserialize)
        {
            XmlSerializer serialize = new XmlSerializer(objecttobeserialize.GetType());
            using(StringWriter textWriter = new StringWriter())
            {
                serialize.Serialize(textWriter, objecttobeserialize);
                return textWriter.ToString();
            }
        }

        private T Deserialize<T>(string stringtobedeserialized)
        {
            object result;

            XmlSerializer serialize = new XmlSerializer(typeof(T));
            StreamReader reader = new StreamReader(stringtobedeserialized);
            {
                result = serialize.Deserialize(reader);
            }

            return (T)result;
        }
    }
}
