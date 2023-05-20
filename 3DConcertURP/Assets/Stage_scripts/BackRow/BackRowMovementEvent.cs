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

        [MinMaxSlider(-60.0f, 60.0f)]

        // Angle range for light bending foward and backward (angle.x)
        public Vector2 minMaxAngle;

        private BackRow_Controller controller;
        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(WideEvent, WideMovement);
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
            //play animations
            if (evt.HasTextPayload())
            {
                string str = evt.GetTextValue();
                if (str == "underwater")
                    controller.PlayUnderwater();
                else if (str == "wavesM")
                    controller.PlayMiddleWave();
                else if (str == "allBright")
                    controller.AllBright();
                else if (str == "allDim")
                    controller.AllDim();
                else if (str == "FB")
                    controller.FrontBack();
                else
                    Debug.Log("BackRow: String doesn't match parameter names.");
            }

            //change animation speed when there's float payload
            if (evt.HasFloatPayload())
            {
                controller.changeAnimSpeed(evt.GetFloatValue());
            }

            else if (evt.HasCurvePayload())
            {
                controller.changeAnimSpeed(evt.GetValueOfCurveAtTime(sampleTime));
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
