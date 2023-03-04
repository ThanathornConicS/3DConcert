using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.Demos
{
    public class BackRowIntensityEvent : MonoBehaviour
    {
        [EventID]
        public string WideEvent;
        [EventID]
        public string NarrowEvent;

        private BackRow_Controller controller;

        private float[] wRandom;
        private float[] nRandom;
        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(WideEvent, WideIntensity);
            Koreographer.Instance.RegisterForEventsWithTime(NarrowEvent, NarrowIntensity);
            controller = this.GetComponent<BackRow_Controller>();
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

        void WideIntensity(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                // Get the value of the curve at the current audio position.  This will be a
                //  value between [0, 1] and will be used, below, to interpolate between
                //  minScale and maxScale.
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
            }
        }
        
        void NarrowIntensity(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                // Get the value of the curve at the current audio position.  This will be a
                //  value between [0, 1] and will be used, below, to interpolate between
                //  minScale and maxScale.
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);

            }
        }
    }

}