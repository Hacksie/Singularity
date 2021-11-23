using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Effector : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private new Collider2D collider;
        [SerializeField] public AreaEffector2D effector;
        [SerializeField] private Color off;
        [SerializeField] private Color over;
        [SerializeField] private Color on;

        public bool isOver = false;

        private Animator animator;

        float clearTimer = 0;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

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
            //sprite.color = over;
            isOver = true;

        }

        public void On()
        {
            effector.enabled = true;
            effector.forceMagnitude = 2 + (-1 * Physics2D.gravity.y);
            clearTimer = Time.time + 0.2f;
            sprite.color = on;
        }

        public void Off()
        {
            effector.enabled = false;
            sprite.color = off;
            isOver = false;    

        }

        void Update()
        {
            Animate();
        }

        void Animate()
        {
            animator.SetBool("Play", effector.enabled || isOver);
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
            //Debug.Log("Exit");
            if(other.CompareTag("Ball"))
            {
                Off();
            }            
        }
    }
}