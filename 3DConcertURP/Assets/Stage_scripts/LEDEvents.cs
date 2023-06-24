using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.Demos
{
    public class LEDEvents : MonoBehaviour
    {
        [EventID]
        public string eventLED;
        private string m_event;

        LightController viewsLight;

        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(eventLED, LEDevent);
            viewsLight = this.GetComponent<LightController>();
            m_event = "";
        }

        void LEDevent(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if(evt.HasTextPayload())
            {
                m_event = evt.GetTextValue();

                if (m_event == "onoff")
                    viewsLight.SetLEDLightLightStage(LightController.LightStage.onAndOff);
                else if (m_event == "RB")
                    viewsLight.SetLEDLightLightStage(LightController.LightStage.rainbowWave);
                else if (m_event == "fade")
                    viewsLight.SetLEDLightLightStage(LightController.LightStage.fadeBetweenThreeColor);
                else if (m_event == "n")
                    viewsLight.SetLEDLightLightStage(LightController.LightStage.none);
                else
                    Debug.Log("[LED events] Invalid text: " + m_event);
            }

            else if(evt.HasFloatPayload())
            {
                viewsLight.SetLEDLightSpeed(evt.GetFloatValue());
            }
        }

        
    }
}