using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Ball : MonoBehaviour
    {
        private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] public bool dropped = false;
        [SerializeField] private float matchRadius = 1.0f;

        public Color Color { get { return sprite.color; } }

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.Sleep();
        }

        public void SetColor(Color color)
        {
            sprite.color = color;
        }

        public void Drop()
        {
            rigidbody.WakeUp();
            dropped = true;
        }



        public bool IsStopped() => rigidbody.velocity.sqrMagnitude < Vector2.kEpsilonNormalSqrt;

        public List<Ball> FindNearbyMatches(List<Ball> current)
        {
            Collider2D[] all = Physics2D.OverlapCircleAll(this.transform.position, matchRadius);
            List<Ball> matches = new List<Ball>();

            foreach(var collider in all)
            {
                if(collider.CompareTag("Ball"))
                {
                    var ball = collider.gameObject.GetComponent<Ball>();
                    if(current.Contains(ball)) // Ignore it if it's already in the list
                    {
                        continue;
                    }

                    if(ball.Color == Color)
                    {
                        matches.Add(ball);
                    }
                }
            }

            current.AddRange(matches);

            foreach(var match in matches)
            {
                current = match.FindNearbyMatches(current);
            }

            return current;
        }


        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Ball"))
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.Sleep();

                if(transform.position.y > 2.25f)
                {
                    Game.Instance.SetGameOver();
                }

                var newList = new List<Ball>();
                newList.Add(this);

                var matches = FindNearbyMatches(newList);

                int factor = 1;
                if(matches.Count >= 3)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        Ball match = matches[i];
                        match.gameObject.SetActive(false);
                        Destroy(match.gameObject);
                        if(i > 3)
                        {
                            factor++;
                        }
                    }
                    Game.Instance.InceaseScore(300 * factor);
                }

                
            }
        }
    }
}