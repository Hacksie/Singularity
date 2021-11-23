using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private float baseSpawnTime = 2.0f;
        [SerializeField] List<Color> colors = new List<Color>();
        [SerializeField] List<Ball> spawnedBalls = new List<Ball>();

        private Ball nextBall;
        private Ball currentBall;

        public Ball CurrentBall { get => currentBall; set => currentBall = value; }

        private float timer = 0;

        void Start()
        {
            //SpawnBall();
        }

        void Update()
        {
            if (Time.time > timer)
            {
                SpawnBall();
            }
        }

        public void Reset()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public void DropNextBall()
        {
            CurrentBall = nextBall;
            CurrentBall.Drop();
            var gravity = Physics2D.gravity;
            gravity = new Vector2(gravity.x, gravity.y - 0.01f);
            Physics2D.gravity = gravity;
            timer = Time.time + baseSpawnTime;
            AudioManager.Instance.PlayDrop();
        }


        public void SpawnBall()
        {
            // Spawn, hold then drop
            var go = Instantiate(ballPrefab.gameObject, this.transform.position, Quaternion.identity, this.transform);
            nextBall = go.GetComponent<Ball>();
            var color = colors[Random.Range(0, colors.Count)];
            nextBall.SetColor(color);
            timer = float.MaxValue;
            spawnedBalls.Add(nextBall);
        }
    }
}