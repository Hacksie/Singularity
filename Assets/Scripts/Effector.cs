using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Effector : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private new Collider2D collider;
        [SerializeField] private Effector2D effector;
        [SerializeField] private Color off;
        [SerializeField] private Color over;
        [SerializeField] private Color on;

        float clearTimer = 0;

        void Start()
        {
            Off();
        }

        public void UpdateColor(Color color)
        {
            sprite.color = color;
        }

        public void Over()
        {
            sprite.color = over;
        }

        public void On()
        {
            //collider.enabled = true;
            effector.enabled = true;
            clearTimer = Time.time + 0.1f;
            sprite.color = on;
        }

        public void Off()
        {
            //collider.enabled = false;
            effector.enabled = false;
            sprite.color = off;

        }

        void Update()
        {
            // if(collider.enabled && Time.time > clearTimer)
            // {
            //     Off();
            // }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            //Debug.Log("Stay");
            if(other.CompareTag("Ball") && !effector.enabled)
            {
                Over();
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Exit");
            if(other.CompareTag("Ball"))
            {
                Off();
            }            
        }
    }
}