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

        [SerializeField]
        Transform performer;

        [SerializeField]
        Transform OutsideDome;

        float t;
        Vector3 newPos;
        Vector3 originalPos;

        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(eventMagicCircle, MagicEvent);
            AttachMagicCircle();
            originalPos = this.transform.position;
        }

        void MagicEvent(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                //calc new position using lerp
                t = evt.GetValueOfCurveAtTime(sampleTime);

                newPos.x = this.transform.position.x;
                newPos.y = Mathf.Lerp(originalPos.y, OutsideDome.position.y, t);
                newPos.z = this.transform.position.z;

                //set new position
                this.transform.position = newPos;
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