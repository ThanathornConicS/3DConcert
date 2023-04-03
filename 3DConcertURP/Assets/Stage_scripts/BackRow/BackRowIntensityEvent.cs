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

        LightPosition backrowSelect;
        LightAction narrowAction;
        LightAction wideAction;

        LightPosition current_set;
        LightAction current_action;

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
            if (evt.HasLightCallerPayload())
            {
                current_action = evt.GetLightAction();
                current_set = evt.GetLightPos();
            }
            if (evt.HasCurvePayload() && current_action == LightAction.intensity
                && current_set == LightPosition.Wide)
            {
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
                for (int i = 0; i < controller.WideSpotlights.Length; i++)
                    controller.ChangeIntensityWide(i, curveValue);
            }
            else if (evt.HasCurvePayload() && current_action == LightAction.underwater
                && current_set == LightPosition.all)
            {
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
                controller.PlayUnderwater(curveValue);
            }
            else if (evt.HasCurvePayload() && current_action == LightAction.middle_wave
                && current_set == LightPosition.all)
            {
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
                controller.PlayMiddleWave(curveValue);
            }
            else if (evt.HasCurvePayload() && current_action == LightAction.underwater_to_halfBright
                && current_set == LightPosition.all)
            {
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
                controller.PlayUnderwater_to_half_bright(curveValue);
            }
        }
        
        void NarrowIntensity(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload() && current_action == LightAction.intensity
                && current_set == LightPosition.Narrow)
            {
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
                for (int i = 0; i < controller.NarrowSpotlights.Length; i++)
                    controller.ChangeIntensityNarrow(i, curveValue);
            }
        }
    }

}