using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


namespace SonicBloom.Koreo.Demos
{
    public class PlayMusic_ : MonoBehaviour
    {
        AudioSource m_audioSource;

        private void Start()
        {
            m_audioSource = this.GetComponent<AudioSource>();
        }

        [Button]
        void PlayLoadedMusic()
        {
            m_audioSource.Play();
        }
    }

}