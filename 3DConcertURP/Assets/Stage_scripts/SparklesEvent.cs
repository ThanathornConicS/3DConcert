using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SonicBloom.Koreo.Demos
{
    public class SparklesEvent : MonoBehaviour
    {
        [EventID]
        public string eventTrigger;

        [SerializeField]
        private int ParticleID = 1;

        [SerializeField]
        ParticleSystem m_Particles;

        void Start()
        {
            Koreographer.Instance.RegisterForEventsWithTime(eventTrigger, TriggerParticles);
            m_Particles = this.GetComponent<ParticleSystem>();

        }

        void TriggerParticles(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            if (evt.HasIntPayload())
            {
                if (evt.GetIntValue() == ParticleID)
                {
                    //if has in playload, trigger sparkles
                    StartRaining();
                }
            }
        }

        [Button]
        void StartRaining()
        {
            m_Particles.Stop();

            var particlesMainSettings = m_Particles.main;
            particlesMainSettings.loop ^= true;

            m_Particles.Play();
        }
    }
}