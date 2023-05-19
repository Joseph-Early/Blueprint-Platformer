using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        private AudioSource audioSource;
        [SerializeField] AudioClip[] backgroundTracks;

        private int currentTrack = 0;

        // Get a reference to the audio source on awake
        private void Awake()
        {
            // Get the audio source component
            audioSource = GetComponent<AudioSource>();

            // Check if the audio source component is null
            if (audioSource is null)
                throw new System.Exception("AudioSource component cannot be found!");

            // Prevent the audio manager from being destroyed on scene load
            DontDestroyOnLoad(gameObject);
        }

        // On Start, start playing first track of background music
        private void Start()
        {
            if (backgroundTracks.Length > 0)
            {
                StartCoroutine(BackgroundTrackCycle());
            }
        }

        // Play background tracks
        private IEnumerator BackgroundTrackCycle()
        {
            currentTrack = 0;
            while (true)
            {
                // Set the audio clip to the new audio clip in the queue
                audioSource.clip = backgroundTracks[currentTrack];

                // Play the audio
                audioSource.Play();

                // Calculate for how long to yield for
                var duration = backgroundTracks[currentTrack].length;

                // Increase track counter
                currentTrack++;
                if (currentTrack >= backgroundTracks.Length)
                    currentTrack = 0;

                yield return new WaitForSeconds(duration);
            }
        }
    }
}