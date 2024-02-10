using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Creates a deep clone of the object using binary serialisation.
        /// </summary>
        /// <typeparam name="T">The type of the object to be cloned.</typeparam>
        /// <param name="item">The object to be cloned.</param>
        /// <returns>The clone of the object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="item" /> is null. </exception>
        public static T DeepClone<T>(this T item)
        {
            return item.ToByteArray().FromByteArray<T>();
        }

        /// <summary>
        ///     Serialises an object, or graph of objects with the given root to a byte array.
        /// </summary>
        /// <param name="serializableObject">
        ///     The object, or root of the object graph, to serialise. All child objects of this root
        ///     object are automatically serialized.
        /// </param>
        /// <returns>A new byte array representing the serialized object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="serializableObject" /> is null</exception>
        public static byte[] ToByteArray(this object serializableObject)
        {
            var formatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                formatter.Serialize(memoryStream, serializableObject);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        ///     Deserialises the data on the provided byte array and reconstitutes the graph of objects.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialise and return.</typeparam>
        /// <param name="serializedObject">The byte array that contains the data to deserialize.</param>
        /// <returns>The top object of the deserialized graph.</returns>
        /// <exception cref="System.ArgumentNullException">the <paramref name="serializedObject" /> is null.</exception>
        public static T FromByteArray<T>(this byte[] serializedObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                memoryStream.Write(serializedObject, 0, serializedObject.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var result = (T)formatter.Deserialize(memoryStream);
                return result;
            }
        }
    }
}
