using System.Runtime.Serialization;
using UnityEngine;

sealed class Texture2DSurrogate : ISerializationSurrogate {
	
	// Method called to serialize a Vector3 object
	public void GetObjectData(System.Object obj,
	                          SerializationInfo info, StreamingContext context) {
		
		Texture2D t = (Texture2D) obj;
	}
	
	// Method called to deserialize a Vector3 object
	public System.Object SetObjectData(System.Object obj,
	                                   SerializationInfo info, StreamingContext context,
	                                   ISurrogateSelector selector) {
		
		Texture2D t = (Texture2D) obj;
		return obj;
	}
}