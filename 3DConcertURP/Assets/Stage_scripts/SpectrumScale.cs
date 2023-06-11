using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SonicBloom.Koreo.Demos
{
    public class SpectrumScale : MonoBehaviour
    {
        [EventID]
        public string eventScale;

        Transform scale;

        [SerializeField]
        float maxT;
        //song 1 (starman!!!) = 0.35

        [MinMaxSlider(0.2f, 3.0f)]
        // Angle range for light bending foward and backward (angle.x)
        public Vector2 minMaxScale;

        // Start is called before the first frame update
        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(eventScale, Scaling);
            scale = this.GetComponent<Transform>();
        }

        float InterpolatingScale(float t)
        {
            float res = 1.0f;
            //has to adjust t first, because t value is b/w [0, max t]
            t /= maxT;
            //Debug.Log("t: " + t);

            res = Mathf.Lerp(minMaxScale.x, minMaxScale.y, t);
            return res;
        }

        void Scaling(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                Debug.Log("Here");
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
                Vector3 tmpVec = new Vector3(
                    InterpolatingScale(curveValue),
                    InterpolatingScale(curveValue),
                    InterpolatingScale(curveValue)
                    );

                scale.localScale = tmpVec;
            }
        }
    }
}