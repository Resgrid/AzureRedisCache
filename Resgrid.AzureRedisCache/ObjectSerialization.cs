using System;
using System.IO;
using ProtoBuf;

namespace Resgrid.AzureRedisCache
{
	public static class ObjectSerialization
	{
		/// <summary>
		/// Serializes and object using Protobuf
		/// </summary>
		/// <param name="o">Object to serialize</param>
		/// <returns>String based result of serialization</returns>
		public static string Serialize(object o)
		{
			String XmlizedString = null;
			var memoryStream = new MemoryStream();

			Serializer.Serialize(memoryStream, o);
			string stringBase64 = Convert.ToBase64String(memoryStream.ToArray());

			return stringBase64;
		}

		/// <summary>
		/// Deserializes a Protobuf serialized object
		/// </summary>
		/// <typeparam name="T">Type of object being Deserialized</typeparam>
		/// <param name="o">Object to Deserialize</param>
		/// <returns>Hydrated object via Deserialization</returns>
		public static T Deserialize<T>(string o)
		{
			byte[] byteAfter64 = Convert.FromBase64String(o);
			MemoryStream memoryStream = new MemoryStream(byteAfter64);

			return Serializer.Deserialize<T>(memoryStream);
		}
	}
}