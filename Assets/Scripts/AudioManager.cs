using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource bounce;
        [SerializeField] private AudioSource drop;
        [SerializeField] private AudioSource score;
        [SerializeField] private AudioSource gameOver;

        public static AudioManager Instance { get; private set; }


        AudioManager()
        {
            Instance = this;
        }

        public void PlayBounce()
        {
            Debug.Log("Bounce");
            if (bounce != null)
            {
                bounce.Play();
            }
        }

        public void PlayScore()
        {
            if (score != null)
            {
                score.Play();
            }
        }

        public void PlayGameOver()
        {
            if (gameOver != null)
            {
                gameOver.Play();
            }
        }

        public void PlayDrop()
        {
            if (drop != null)
            {
                drop.Play();
            }
        }        
    }
}