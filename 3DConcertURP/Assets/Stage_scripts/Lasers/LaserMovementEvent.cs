using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SonicBloom.Koreo.Demos
{
    public class LaserMovementEvent : MonoBehaviour
    {
        [EventID]
        public string eventMovement;
        private LaserController controller;

        [SerializeField]
        private Transform lineupLT;

        [HorizontalLine(color: EColor.Yellow)]

        // Angle range for light bending foward and backward
        [Foldout("Wave Settings")]
        [MinMaxSlider(-50.0f, 60.0f)]
        public Vector2 minMaxAngle;

        /// <summary>
        /// Angle difference between the current and previous laser when the wave pattern is played.
        /// </summary>
        [Foldout("Wave Settings")]
        [SerializeField]
        float offsetAngle = 5.0f;

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

        string pattern;
        //NA
        //waves
        //fan
        //fanL
        //lean
        //dimming
        //off
        //on

        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(eventMovement, LaserMovement);
            controller = gameObject.GetComponent<LaserController>();

            //Init dimming array
            sourceMaxArr = new float[controller.lasersNum];

            if (SourceMax != -1)
            {
                for (int i = 0; i < controller.lasersNum; i++)
                    sourceMaxArr[i] = SourceMax;
            }

            filterArr = new float[FilterSize];
            for (int i = 0; i < FilterSize; i++)
                filterArr[i] = Mathf.Sin((i / ((float)FilterSize - 1)) * Mathf.PI);
            filterArr[FilterSize - 1] = filterArr[0];
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

        void LaserMovement(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasCurvePayload())
            {
                // Get the value of the curve at the current audio position.  This will be a
                //  value between [0, 1] and will be used, below, to interpolate between
                //  minScale and maxScale.
                float curveValue = evt.GetValueOfCurveAtTime(sampleTime);

                controller.LaserOn();
                if (pattern == "fan")
                    FanningMiddle(curveValue);
                else if (pattern == "waves")
                    Wave(curveValue);
                else if (pattern == "lean")
                    FrontBackLean(curveValue);
                else if (pattern == "fanL")
                    FanningSideL(curveValue);
                else if (pattern == "fanR")
                    FanningSideR(curveValue);
                else if (pattern == "dimming")
                    DimmingPattern(curveValue);
                else
                    Debug.Log("Laser: String doesn't match parameter names: " + pattern);
                //transform.localScale = Vector3.one * Mathf.Lerp(minScale, maxScale, curveValue);
            }

            if (evt.HasTextPayload())
            {
                pattern = evt.GetTextValue();

                if (pattern == "off")
                {
                    //also reset line up angles
                    lineupLT.eulerAngles = new Vector3(0.0f,
                        lineupLT.eulerAngles.y,
                        lineupLT.eulerAngles.z);
                    controller.LaserOff();
                }
                else if (pattern == "on")
                    controller.LaserOn();
                else if (pattern == "lean")
                {
                    //force fan before lean
                    ////Space between each lasesr is 18 degree
                    ////starting angle 90
                    float angle = 90.0f;
                    float increment = 15.0f;
                    float finalRot = 0.0f;

                    for (int i = 0; i < controller.lasersNum; i++)
                    {
                        //calculate final rotation value
                        finalRot = angle - (increment * (i + 1));

                        //Left
                        controller.LaserbeamsL[i].transform.localEulerAngles = new Vector3
                            (controller.LaserbeamsL[i].transform.localEulerAngles.x,
                            Mathf.Lerp(0, finalRot, 1.0f),
                            controller.LaserbeamsL[i].transform.localEulerAngles.z);
                    }
                }
                else if (pattern == "fanL")
                {
                    Debug.Log("Text: fanL");
                }
                else if (pattern == "fanR")
                {
                    Debug.Log("Text: fanR");
                }

            }
        }

        //Lasers patterns
        void Wave(float t)
        {
            float offsetInterval = angleToOffset(offsetAngle, minMaxAngle.x, minMaxAngle.y);
            for (int i = 0; i < controller.lasersNum; i++)
            {
                float interval = (t - (i * offsetInterval)) % 1.0f;
                if (interval < 0.0f) interval += 1.0f;

                //if(i == 0) Debug.Log(calcInterval(interval));

                float tmp = Mathf.Lerp(minMaxAngle.x, minMaxAngle.y, calcInterval(interval));
                controller.LaserbeamsL[i].transform.localEulerAngles = new Vector3(
                    tmp,
                    controller.LaserbeamsL[i].transform.localEulerAngles.y,
                    controller.LaserbeamsL[i].transform.localEulerAngles.z);

                //LaserbeamsR[i].transform.localEulerAngles = new Vector3(
                //    tmp,
                //    LaserbeamsR[i].transform.localEulerAngles.y,
                //    LaserbeamsR[i].transform.localEulerAngles.z);
            }
        }

        void FanningMiddle(float t) //from middle
        {
            //Space between each lasesr is 18 degree
            //starting angle 90
            float angle = 90.0f;
            float increment = 15.0f;
            float finalRot = 0.0f;

            for (int i = 0; i < controller.lasersNum; i++)
            {
                //calculate final rotation value
                finalRot = angle - (increment * (i + 1));

                //Left
                controller.LaserbeamsL[i].transform.localEulerAngles = new Vector3
                    (controller.LaserbeamsL[i].transform.localEulerAngles.x,
                    Mathf.Lerp(0, finalRot, t),
                    controller.LaserbeamsL[i].transform.localEulerAngles.z);
            }
        }

        void FanningSideL(float t)
        {
            //right angle.y = -40
            //left  angle.x = 40
            for (int i = 0; i < controller.lasersNum; i++) 
            {
                controller.LaserbeamsL[i].transform.localEulerAngles = new Vector3
                (controller.LaserbeamsL[i].transform.localEulerAngles.x,
                 Mathf.Lerp(-40.0f - ((controller.lasersNum - i)*3), 40.0f + (i*3), (t * ((float)controller.lasersNum / (i + 1.0f)))),
                 controller.LaserbeamsL[i].transform.localEulerAngles.z);

                //Debug.Log("i: " + i + " " + Mathf.Lerp(-40.0f, 40.0f, (t * (controller.lasersNum / (i + 1)))));
            }
            //Debug.Log("fanning L");

        }

        void FanningSideR(float t)
        {
            //right angle.y = -40
            //left  angle.x = 40
            for (int i = controller.lasersNum - 1; i >= 0; i--)
            {
                controller.LaserbeamsL[i].transform.localEulerAngles = new Vector3
                (controller.LaserbeamsL[i].transform.localEulerAngles.x,
                 Mathf.Lerp(40.0f + (i * 3), -40.0f - ((controller.lasersNum - i) * 3), (t * ((float)controller.lasersNum / (controller.lasersNum - i)))),
                 controller.LaserbeamsL[i].transform.localEulerAngles.z);
            }
            //Debug.Log("fanning R");
        }

        void FrontBackLean(float t)
        {
            lineupLT.eulerAngles = new Vector3 (Mathf.Lerp(minMaxAngle.x, minMaxAngle.y, t),
                lineupLT.eulerAngles.y,
                lineupLT.eulerAngles.z);
        }

        //for Wave pattern
        float calcInterval(float t)
        {
            return (Mathf.Sin(Mathf.PI * (2 * t - 1)) + 1) * 0.5f;
        }
        //for Wave pattern
        float angleToOffset(float degOffset, float degMin, float degMax)
        {
            return degOffset / (degMax - degMin);
        }

        void DimmingPattern(float t)
        {
            //string debugString = "";
            float tau = Mathf.Lerp(0, controller.lasersNum + FilterSize, t);

            //Convolution
            for (int i = 0; i < controller.lasersNum; i++)
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

