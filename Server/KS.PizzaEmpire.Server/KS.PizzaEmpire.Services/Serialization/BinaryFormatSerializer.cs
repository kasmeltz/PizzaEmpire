namespace KS.PizzaEmpire.Services.Serialization
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Represents an item that uses a BinaryFormatter to serialize 
    /// and deserialize objects to and from byte streams.
    /// </summary>
    public class BinaryFormatSerializer : ICacheSerializer
    {
        BinaryFormatter Formatter { get; set; }

        public BinaryFormatSerializer()
        {
            Formatter = new BinaryFormatter();
        }
        
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
                Formatter.Serialize(memoryStream, o);
                return  memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Deserialize an instance of an object from a byte stream.
        /// </summary>
        /// <param name="stream">A byte stream containing the instance ofthe object.</param>
        /// <returns>An instance of an object contained in the byte stream.</returns>
        public T Deserialize<T>(byte [] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                return (T)Formatter.Deserialize(memoryStream);
            }
        }
    }
}
