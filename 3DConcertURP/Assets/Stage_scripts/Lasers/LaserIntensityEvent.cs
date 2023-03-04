using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SonicBloom.Koreo.Demos
{
    public class LaserIntensityEvent : MonoBehaviour
    {
        [EventID]
        public string eventIntensity;
        private LaserController controller;

        [HorizontalLine(color: EColor.Yellow)]

        //Dimming variables
        [Foldout("Dimming Sequence Settings")]
        [SerializeField]
        private float SourceMax = 1.0f;

        [Foldout("Dimming Sequence Settings")]
        [SerializeField]
        //must be even number
        private int FilterSize = 5;
        private float[] sourceMaxArr;
        private float[] filterArr;

        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(eventIntensity, LaserIntensity);
            controller = gameObject.GetComponent<LaserController>();

            //Init dimming array
            sourceMaxArr = new float[controller.lasersNum];

            if(SourceMax != -1)
            {
                for (int i = 0; i < controller.lasersNum; i++)
                    sourceMaxArr[i] = SourceMax;
            }

            filterArr = new float[FilterSize];
            for (int i = 0; i < FilterSize; i++)
                filterArr[i] = Mathf.Sin((i/ ((float)FilterSize - 1)) * Mathf.PI);
            filterArr[FilterSize-1] = filterArr[0];
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

        void LaserIntensity(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                // Get the value of the curve at the current audio position.  This will be a
                //  value between [0, 1] and will be used, below, to interpolate between
                //  minScale and maxScale.
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);

                DimmingPattern(curveValue);
                //transform.localScale = Vector3.one * Mathf.Lerp(minScale, maxScale, curveValue);
            }
        }

        void DimmingPattern(float t)
        {
            //string debugString = "";
            float tau = Mathf.Lerp(0, controller.lasersNum + FilterSize, t);

            //Convolution
            for(int i = 0; i < controller.lasersNum; i++)
            {
                float delta = tau - i;

                int floorDelta = ((int)Mathf.Floor(delta));
                int ceilDelta = ((int)Mathf.Ceil(delta));

                // clamp to prevent out of bounds : filterArr[0] == filterArr[n] == 0
                int floorIndex = Mathf.Clamp(floorDelta, 0, FilterSize - 1);
                int ceilIndex = Mathf.Clamp(ceilDelta, 0, FilterSize - 1);

                // intensity = filter[delta] * sourceMaxArr[i];
                float tmp = Mathf.Lerp(filterArr[floorIndex], filterArr[ceilIndex], delta - floorDelta) * sourceMaxArr[i];
                controller.ChangeIntensity(i, tmp);
            }

        }

        void JustIntensity(float t)
        {
            for (int i = 0; i < controller.lasersNum; i++)
            {
                controller.ChangeIntensity(i, t);
            }
        }
    }

}