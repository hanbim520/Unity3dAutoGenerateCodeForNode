using System.Runtime.Serialization;
using UnityEngine;

sealed class TransformSurrogate : ISerializationSurrogate {
	
	// Method called to serialize a Vector3 object
	public void GetObjectData(System.Object obj,
	                          SerializationInfo info, StreamingContext context) {
		
		Transform tr = (Transform) obj;
	}
	
	// Method called to deserialize a Vector3 object
	public System.Object SetObjectData(System.Object obj,
	                                   SerializationInfo info, StreamingContext context,
	                                   ISurrogateSelector selector) {
		
		Transform tr = (Transform) obj;
		
		return obj;
	}
}