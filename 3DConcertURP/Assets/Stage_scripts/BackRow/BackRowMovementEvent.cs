using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SonicBloom.Koreo.Demos
{
    public class BackRowMovementEvent : MonoBehaviour
    {
        enum Pattern { underwater, middle_wave, towards_audience };

        // Start is called before the first frame update
        [EventID]
        public string WideEvent;
        [EventID]
        public string NarrowEvent;

        [SerializeField]
        public Animation [] animations;

        [MinMaxSlider(-60.0f, 60.0f)]

        // Angle range for light bending foward and backward (angle.x)
        public Vector2 minMaxAngle;

        private BackRow_Controller controller;
        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(WideEvent, WideMovement);
            Koreographer.Instance.RegisterForEventsWithTime(NarrowEvent, NarrowMovement);
            controller = this.GetComponent<BackRow_Controller>();

            minMaxAngle.x = -26.0f;
            minMaxAngle.y = 26.0f;
        }
        void OnDestroy()
        {
            // Sometimes the Koreographer Instance gets cleaned up before hand.
            //  No need to worry in that case.
            if (Koreographer.Instance != null)
            {
                Koreographer.Instance.UnregisterForAllEvents(this);
            }
        }

        void WideMovement(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                // Get the value of the curve at the current audio position.  This will be a
                //  value between [0, 1] and will be used, below, to interpolate between
                //  minScale and maxScale.
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);

                //transform.localScale = Vector3.one * Mathf.Lerp(minScale, maxScale, curveValue);
            }
        }

        void NarrowMovement(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                // Get the value of the curve at the current audio position.  This will be a
                //  value between [0, 1] and will be used, below, to interpolate between
                //  minScale and maxScale.
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);

                //transform.localScale = Vector3.one * Mathf.Lerp(minScale, maxScale, curveValue);
            }
        }

        void wFrontBack(float t)
        {
            for (int i = 0; i < controller.WideSpotlights.Length; i++)
            {
                controller.WideSpotlights[i].transform.localEulerAngles = new Vector3
                (
                    Mathf.Lerp(minMaxAngle.x, minMaxAngle.y, t),
                    controller.WideSpotlights[i].transform.localEulerAngles.y,
                    controller.WideSpotlights[i].transform.localEulerAngles.z
                );
            }
        }
        
        void nFrontBack(float t)
        {
            for (int i = 0; i < controller.NarrowSpotlights.Length; i++)
            {
                controller.NarrowSpotlights[i].transform.localEulerAngles = new Vector3
                (
                    Mathf.Lerp(minMaxAngle.x, minMaxAngle.y, t),
                    controller.NarrowSpotlights[i].transform.localEulerAngles.y,
                    controller.NarrowSpotlights[i].transform.localEulerAngles.z
                );
            }
        }
    }
}
