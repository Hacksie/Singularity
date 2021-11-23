using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            var matches = Physics2D.OverlapCircleAll(this.transform.position, matchRadius).Where(c => c.CompareTag("Ball")).Select(c => c.GetComponent<Ball>()).Where(b => b.Color == Color && !current.Contains(b));
            current.AddRange(matches);

            foreach (var match in matches)
            {
                current = match.FindNearbyMatches(current);
            }

            return current;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Ball"))
            {
                Stop();
            }

            if (collision.gameObject.CompareTag("Bounce"))
            {
                AudioManager.Instance.PlayBounce();
            }
        }

        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.Sleep();

            if (IsGameOver())
            {
                AudioManager.Instance.PlayGameOver();
                Game.Instance.SetMenu();
            }

            var newList = new List<Ball>();
            newList.Add(this);

            var matches = FindNearbyMatches(newList);

            int factor = 1;
            if (matches.Count >= 3)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    Ball match = matches[i];
                    match.gameObject.SetActive(false);
                    Destroy(match.gameObject);
                    if (i > 3)
                    {
                        factor++;
                    }
                }
                Game.Instance.CurrentScore += (300 * factor);
                AudioManager.Instance.PlayScore();
            }
        }

        private bool IsGameOver() => transform.position.y > 2.25f;
    }
}