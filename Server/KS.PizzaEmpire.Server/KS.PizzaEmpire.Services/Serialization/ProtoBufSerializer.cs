using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.Services.Serialization
{
    /// <summary>
    /// Represents an item that uses a ProtoBuf to serialize 
    /// and deserialize objects to and from byte streams.
    /// </summary>
    public class ProtoBufSerializer: ICacheSerializer
    {
        /// <summary>
        /// Serialize an instance of an object to a byte stream.
        /// </summary>
        /// <param name="o">The instance to serialize</param>
        /// <returns>A byte stream repressenting the instance of the object.</returns>
        public byte[] Serialize(object o)
        {
            if (o == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, o);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Deserialize an instance of an object from a byte stream.
        /// </summary>
        /// <param name="stream">A byte stream containing the instance ofthe object.</param>
        /// <returns>An instance of an object contained in the byte stream.</returns>
        public T Deserialize<T>(byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                return Serializer.Deserialize<T>(memoryStream);
            }
        }
    }
}
