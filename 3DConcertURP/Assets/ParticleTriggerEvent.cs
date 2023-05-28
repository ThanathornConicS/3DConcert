using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.Demos
{
    public class ParticleTriggerEvent : MonoBehaviour
    {
        [EventID]
        public string eventTrigger;

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
                //if has in playload, trigger confetti
                m_Particles.Play();
            }
        }
    }
}