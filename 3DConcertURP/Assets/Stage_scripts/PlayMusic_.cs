using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


namespace SonicBloom.Koreo.Demos
{
    public class PlayMusic_ : MonoBehaviour
    {
        private AudioSource m_audioSource;
        private float wait;

        private void Start()
        {
            m_audioSource = this.GetComponent<AudioSource>();
            wait = 6.48f;
            StartCoroutine(ExampleCoroutine());
        }

        [Button]
        void PlayLoadedMusic()
        {
            m_audioSource.Play();
        }
        
        IEnumerator ExampleCoroutine()
        {
            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(wait);

            m_audioSource.Play();
            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        }

    }

}