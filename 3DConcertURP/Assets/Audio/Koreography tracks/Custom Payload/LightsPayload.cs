using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Extension Methods for the <see cref="KoreographyEvent"/> class that add
/// <see cref="LightsPayload"/>-specific functionality.
/// </summary>
/// 

namespace SonicBloom.Koreo.Demos
{
	public static class LightsPayloadEventExtensions
	{
		#region KoreographyEvent Extension Methods

		/// <summary>
		/// Determines if the payload is of type <see cref="LightsPayload"/>.
		/// </summary>
		/// <returns><c>true</c> if the payload is of type <see cref="LightsPayload"/>;
		/// otherwise, <c>false</c>.</returns>
		public static bool HasLightCallerPayload(this KoreographyEvent koreoEvent)
		{
			return (koreoEvent.Payload as LightsPayload) != null;
		}

		/// <summary>
		/// Returns the asset reference associated with the LightsPayload.  If the
		/// Payload is not actually of type <see cref="LightsPayload"/>, this will return
		/// null.
		/// </summary>
		/// <returns>The <c>asset reference</c>.</returns>
		public static LightCaller GetContent(this KoreographyEvent koreoEvent)
		{
			LightCaller retVal = null;

			LightsPayload pl = koreoEvent.Payload as LightsPayload;
			if (pl != null)
			{
				retVal = pl.LightVal;
			}

			return retVal;
		}
		
		public static float GetCurveVal(this KoreographyEvent koreoEvent, int sampleTime)
		{
			float retVal = 0.0f;

			LightsPayload pl = koreoEvent.Payload as LightsPayload;
			if (pl != null)
			{
				retVal = pl.LightVal.AnimCurve.Evaluate(sampleTime);
			}

			return retVal;
		}

		public static LightPosition GetLightPos(this KoreographyEvent koreoEvent)
        {
			LightPosition retVal = 0;

			LightsPayload pl = koreoEvent.Payload as LightsPayload;
			if (pl != null)
			{
				retVal = pl.LightVal.CallLight;
			}

			return retVal;
		}

		public static LightAction GetLightAction(this KoreographyEvent koreoEvent)
        {
			LightAction retVal = 0;

			LightsPayload pl = koreoEvent.Payload as LightsPayload;
			if (pl != null)
			{
				retVal = pl.LightVal.LightFunction;
			}

			return retVal;
		}

		#endregion
	}

	/// <summary>
	/// The LightsPayload class allows Koreorgraphy Events to contain a reference to
	/// an asset as a payload.
	/// </summary>
	[System.Serializable]
	public class LightsPayload : IPayload
	{
		#region Fields

		[SerializeField]
		[Tooltip("The light contents reference stored in the payload.")]
		LightCaller m_LightVal;

		#endregion
		#region Properties

		/// <summary>
		/// Gets or sets the asset reference value.
		/// </summary>
		/// <value>The asset reference value.</value>
		public LightCaller LightVal
		{
			get
			{
				return m_LightVal;
			}
			set
			{
				m_LightVal = value;
			}
		}

		#endregion
		#region Standard Methods

		/// <summary>
		/// This is used by the Koreography Editor to create the Payload type entry
		/// in the UI dropdown.
		/// </summary>
		/// <returns>The friendly name of the class.</returns>
		public static string GetFriendlyName()
		{
			return "Light Caller";
		}

		#endregion
		#region IPayload Interface

#if UNITY_EDITOR

		/// <summary>
		/// Used for drawing the GUI in the Editor Window (possibly scene overlay?).  Undo is
		/// supported.
		/// </summary>
		/// <returns><c>true</c>, if the Payload was edited in the GUI, <c>false</c>
		/// otherwise.</returns>
		/// <param name="displayRect">The <c>Rect</c> within which to perform GUI drawing.</param>
		/// <param name="track">The Koreography Track within which the Payload can be found.</param>
		/// <param name="isSelected">Whether or not the Payload (or the Koreography Event that
		/// contains the Payload) is selected in the GUI.</param>
		public bool DoGUI(Rect displayRect, KoreographyTrackBase track, bool isSelected)
		{
			bool bDidEdit = false;
			Color originalBG = GUI.backgroundColor;
			GUI.backgroundColor = isSelected ? Color.green : originalBG;

			EditorGUI.BeginChangeCheck();

			float pickerWidth = 20f;
			LightCaller newVal = null;

			// Handle short fields.
			if (displayRect.width >= pickerWidth + 2f)
			{
				// HACK to make the background of the picker icon readable.
				Rect pickRect = new Rect(displayRect);
				pickRect.xMin = pickRect.xMax - pickerWidth;
				GUI.Box(pickRect, string.Empty, EditorStyles.textField);

				// Draw the Light field.
				newVal = EditorGUI.ObjectField(displayRect, LightVal, typeof(LightCaller), false) as LightCaller;
			}
			else
			{
				// Simply show a text field.
				string name = (LightVal != null) ? LightVal.name : "None (Material)";
				string tooltip = isSelected ? "Edit in the \"Selected Event Settings\" section below." : "Select this event and edit in the \"Selected Event Settings\" section below.";
				GUI.Box(displayRect, new GUIContent(name, tooltip), EditorStyles.textField);
			}

			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(track, "Modify Light Caller Payload");
				LightVal = newVal;
				bDidEdit = true;
			}

			GUI.backgroundColor = originalBG;
			return bDidEdit;
		}

		/// <summary>
		/// Used to determine the Payload's desired width for rendering in certain contexts
		/// (e.g. in Peek UI). Return <c>0</c> to indicate a default width.
		/// </summary>
		/// <returns>The desired width for UI rendering or <c>0</c> to use the default.</returns>
		public float GetDisplayWidth()
		{
			return 0f;  // Use default.
		}

#endif

		/// <summary>
		/// Returns a copy of the current object, including the pertinent parts of
		/// the payload.
		/// </summary>
		/// <returns>A copy of the Payload object.</returns>
		public IPayload GetCopy()
		{
			LightsPayload newPL = new LightsPayload();
			newPL.LightVal = LightVal;

			return newPL;
		}

		#endregion
	}
}
