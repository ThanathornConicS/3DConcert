using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


namespace SonicBloom.Koreo.Demos
{
    public class PlayMusic_ : MonoBehaviour
    {
        AudioSource m_audioSource;
        [SerializeField]
        float Soundwait;

        [SerializeField]
        float MOCAPwait;

        [SerializeField]
        Animator bodyAnimator;

        [SerializeField]
        float FaceWait;

        [SerializeField]
        Animator faceAnimator;

        bool oneTime1 = true;
        bool oneTime2 = true;
        bool oneTime3 = true;

        private void Start()
        {
            m_audioSource = this.GetComponent<AudioSource>();
        }

        [Button]
        void PlayLoadedMusic()
        {
            m_audioSource.Play();
        }

        private void Update()
        {
            if (oneTime1)
            {
                StartCoroutine(WaitSound());
                oneTime1 = false;
            }
            if (oneTime2)
            {
                StartCoroutine(WaitMOCAP());
                oneTime2 = false;
            }
            if (oneTime3)
            {
                StartCoroutine(WaitFace());
                oneTime3 = false;
            }
        }

        IEnumerator WaitSound()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(Soundwait);

            PlayLoadedMusic();
            //After we have waited 5 seconds print the time again.
            Debug.Log("Start sound timestamp : " + Time.time);
        }
        
        IEnumerator WaitMOCAP()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(MOCAPwait);

            bodyAnimator.SetBool("Start", true);
            //After we have waited 5 seconds print the time again.
            Debug.Log("Start mocap at timestamp : " + Time.time);
        }
        
        IEnumerator WaitFace()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(FaceWait);

            faceAnimator.SetBool("Start", true);
            //After we have waited 5 seconds print the time again.
            Debug.Log("Start face motion at timestamp : " + Time.time);
        }

    }

}