using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.Demos
{
    public class BackRowGradientEvent : MonoBehaviour
    {
        [EventID]
        public string WideEvent;
        [EventID]
        public string NarrowEvent;

        private BackRow_Controller controller;

        // Start is called before the first frame update
        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(WideEvent, WideGradient);
            Koreographer.Instance.RegisterForEventsWithTime(NarrowEvent, NarrowGradient);
            controller = this.GetComponent<BackRow_Controller>();
        }


        void WideGradient(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasGradientPayload())
            {
                for (int i = 0; i < controller.WideSpotlights.Length; i++)
                {
                    controller.ChangeGradientWide(i, evt.GetGradientData());
                }
            }
        }

        void NarrowGradient(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasGradientPayload())
            {
                for (int i = 0; i < controller.NarrowSpotlights.Length; i++)
                {
                    controller.ChangeGradientNarrow(i, evt.GetGradientData());
                }
            }
        }

    }
}