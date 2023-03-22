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

        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(eventMovement, LaserMovement);
            controller = gameObject.GetComponent<LaserController>();

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

                Wave(curveValue);
                //transform.localScale = Vector3.one * Mathf.Lerp(minScale, maxScale, curveValue);
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

                Debug.Log("i: " + i + " " + Mathf.Lerp(-40.0f, 40.0f, (t * (controller.lasersNum / (i + 1)))));

            }

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
    }
}

