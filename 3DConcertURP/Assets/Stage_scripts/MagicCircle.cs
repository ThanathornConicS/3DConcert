using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


namespace SonicBloom.Koreo.Demos
{
    public class MagicCircle : MonoBehaviour
    {
        [EventID]
        public string eventMagicCircle;

        [EventID]
        public string eventSPLights;

        [SerializeField]
        GameObject aboveLights;

        [SerializeField]
        Transform performer;

        [SerializeField]
        Transform OutsideDome;

        [SerializeField]
        Light[] SP;

        [SerializeField]
        Animator m_animator;
        
        float t;
        Vector3 newPos;
        Vector3 originalPos;
        float originalIntensity;
        float fadeT;

        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(eventMagicCircle, MagicEvent);
            Koreographer.Instance.RegisterForEventsWithTime(eventSPLights, FadeEvent);
            //AttachMagicCircle();
            originalPos = this.transform.position;

            originalIntensity = SP[0].intensity;
        }
        
        void FadeEvent(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                fadeT = evt.GetValueOfCurveAtTime(sampleTime);
                for (int i = 0; i < SP.Length; i++)
                    SP[i].intensity = Mathf.Lerp(0.0f, originalIntensity, fadeT);
            }
        }
        void MagicEvent(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                aboveLights.SetActive(true);
                //calc new position using lerp
                t = evt.GetValueOfCurveAtTime(sampleTime);

                newPos.x = this.transform.position.x;
                newPos.y = Mathf.Lerp(originalPos.y, OutsideDome.position.y, t);
                newPos.z = this.transform.position.z;

                //set new position
                this.transform.position = newPos;

                if (t >= 0.999f)
                {
                    DetatchMagicCircle();
                    //animation: shrink magic circle
                    m_animator.SetTrigger("Shrink");
                    Debug.Log("Start shrink");
                }
            }
        }

            [Button]
        void AttachMagicCircle()
        {
            performer.SetParent(this.transform, true);
        }

        [Button]
        void DetatchMagicCircle()
        {
            performer.transform.parent = null;
        }
    }
}