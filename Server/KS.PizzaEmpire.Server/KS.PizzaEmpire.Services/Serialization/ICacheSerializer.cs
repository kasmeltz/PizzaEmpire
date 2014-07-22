namespace KS.PizzaEmpire.Services.Serialization
{
    /// <summary>
    /// Defines an interface for a cache serializer
    /// </summary>
    public interface ICacheSerializer
    {
        /// <summary>
        /// Serialize an instance of an object to a byte stream.
        /// </summary>
        /// <param name="o">The instance to serialize</param>
        /// <returns>A byte stream representing the instance of the object.</returns>
        byte[] Serialize(object o);

        /// <summary>
        /// Deserialize an instance of an object from a byte stream.
        /// </summary>
        /// <param name="stream">A byte stream containing the instance ofthe object.</param>
        /// <returns>An instance of an object contained in the byte stream.</returns>
        T Deserialize<T>(byte [] stream);
    }
}
