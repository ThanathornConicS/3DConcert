using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SonicBloom.Koreo.Demos
{
    public class LaserController : MonoBehaviour
    {
        [SerializeField]
        public GameObject[] LaserbeamsL;

        [SerializeField]
        private float maxIntensity;

        public float MaxIntensity()
        {
            return maxIntensity;
        }

        //rotation angle
        //X = front back
        //Y = side to side

        private Vector3 OriginalRot;
        public int lasersNum = 0;

        // Start is called before the first frame update
        void Awake()
        {
            //Striaght up
            OriginalRot = new Vector3(0.0f, 0.0f, 0.0f);

            lasersNum = LaserbeamsL.Length;
            maxIntensity = 2.0f;
        }

        [Button]
        public void ResetAngles()
        {
            for (int i = 0; i < lasersNum; i++)
            {
                LaserbeamsL[i].transform.localEulerAngles = OriginalRot;
                //LaserbeamsR[i].transform.eulerAngles = OriginalRot;
            }
        }

        [Button]

        public void ResetIntensity()
        {
            for (int i = 0; i < lasersNum; i++)
            {
                ChangeIntensity(i, 1.0f);
                //LaserbeamsR[i].transform.eulerAngles = OriginalRot;
            }
        }

        public Vector3 defaultRot()
        {
            return OriginalRot;
        }

        public void ChangeIntensity(int i, float intensity) //intensity b/w 0-1
        {
            float val = Mathf.Lerp(0.0f, maxIntensity, intensity);
            LaserbeamsL[i].GetComponent<Light>().intensity = val;
            //LaserbeamsR[i].GetComponent<Light>().intensity = val;
        }

        [Button]
        public void LaserOn()
        {
            for (int i = 0; i < lasersNum; i++)
                ChangeIntensity(i, MaxIntensity());
        }

        [Button]
        public void LaserOff()
        {
            //reset angles, then dim all lights
            ResetAngles();

            for (int i = 0; i < lasersNum; i++)
                ChangeIntensity(i, 0);
        }
    }
}