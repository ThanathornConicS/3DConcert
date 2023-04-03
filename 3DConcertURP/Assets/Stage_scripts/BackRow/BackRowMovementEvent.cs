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

        LightPosition current_set;
        LightAction current_action;

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
            if (evt.HasLightCallerPayload())
            {
                current_action = evt.GetLightAction();
                current_set = evt.GetLightPos();
            }
            if (evt.HasCurvePayload())
            {
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
                if (current_action == LightAction.front_back && current_set == LightPosition.Wide)
                {
                    wFrontBack(curveValue);
                }
            }
        }

        void NarrowMovement(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
                if (current_action == LightAction.front_back && current_set == LightPosition.Narrow)
                {
                    nFrontBack(curveValue);
                }
            }
        }

        void wFrontBack(float t)
        {
            controller.animator.Play("wide_front_back");
        }
        
        void nFrontBack(float t)
        {
            controller.animator.Play("narrow_front_back");
        }
    }
}
