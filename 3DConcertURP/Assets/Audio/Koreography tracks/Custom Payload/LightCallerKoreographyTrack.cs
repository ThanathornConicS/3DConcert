//----------------------------------------------
//            	   Koreographer                 
//    Copyright © 2014-2020 Sonic Bloom, LLC    
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

namespace SonicBloom.Koreo.Demos
{
#if !(UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0)
	// This attribute adds the class to the Assets/Create menu so that it may be
	//	instantiated. [Requires Unity 5.1.0 and up.]
	[CreateAssetMenuAttribute(fileName = "New LightCallerKoreographyTrack", menuName = "Light Caller Koreography Track")]
#endif
	public class LightCallerKoreographyTrack : KoreographyTrack
	{
		#region Serialization Handling

		[HideInInspector][SerializeField]
		protected List<LightsPayload> _LightsPayloads;// List that stores LightsPayload types.
		[HideInInspector][SerializeField]
		protected List<int> _LightsPayloadIdxs;			// List that stores indices of LightsPayload types in the Koreography Track.

		#endregion
	}
}
