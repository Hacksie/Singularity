using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Ball ballPrefab;   
        [SerializeField] private float baseSpawnTime = 2.0f;
        [SerializeField] List<Color> colors = new List<Color>();
        [SerializeField] private Transform parent;

        private Ball currentBall;

        public static Game Instance { get; private set; }

        public Ball CurrentBall { get => currentBall; set => currentBall = value; }

        private IState state = new EmptyState();

        public IState State
        {
            get
            {
                return state;
            }
            private set
            {
                state.End();
                state = value;
                state.Begin();
            }
        }

        Game()
        {
            Instance = this;
        }

        void Start()
        {
            SetPlaying();
        }

        void Update() => state.Update();
        void FixedUpdate() => state.FixedUpdate();

        public void SetPlaying() => State = new PlayingState();

        public void SpawnBall(int color) 
        {
            var go = Instantiate(ballPrefab.gameObject, parent.transform.position, Quaternion.identity, parent);
            currentBall = go.GetComponent<Ball>();            
        }
    }
}