using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


namespace SonicBloom.Koreo.Demos
{
    public class MusicController : MonoBehaviour
    {
        //Event IDs
        [SerializeField]
        string[] StarmanEventIDs;
        [SerializeField]
        string[] Original1EventID;
        [SerializeField]
        string[] Original2EventID;

        //Lights Koreographer
        [SerializeField]
        BackRowMovementEvent backRow;

        [SerializeField]
        SparklesEvent sparkles;

        [SerializeField]
        ConfettiEvent[] confetti;

        [SerializeField]
        LaserMovementEvent[] laser;

        [SerializeField]
        SpectrumScale [] speakers;

        [Button]
        void Starman()
        {
            Debug.Log("Changed song to Starman!!!");

            //Back row
            backRow.WideEvent = StarmanEventIDs[0];
            //Lasers
            laser[0].eventMovement = StarmanEventIDs[1];
            laser[1].eventMovement = StarmanEventIDs[1];
 
            //Particles
            confetti[0].eventTrigger = StarmanEventIDs[2];
            confetti[1].eventTrigger = StarmanEventIDs[2];
            sparkles.eventTrigger = StarmanEventIDs[2];

            //Speakers
            for (int i = 0; i < speakers.Length; i++)
                speakers[i].eventScale = StarmanEventIDs[3];
        }

        [Button]
        void SparklingDreams()
        {
            Debug.Log("Changed song to original song #1 - Sparkling Dream");
            //Speakers
            for (int i = 0; i < speakers.Length; i++)
                speakers[i].eventScale = Original1EventID[0];
        }

        [Button]
        void CowardlyLove()
        {
            Debug.Log("Changed song to original song #2 - Cowardly");
        }
    }
}