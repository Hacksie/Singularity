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
        [SerializeField] private Spawner spawner;
        [Header("UI")]
        [SerializeField] private UI.MenuPresenter menu;
        [SerializeField] private UI.HudPresenter hud;
        [Header("State")]
        [SerializeField] private int currentScore = 0;
        [SerializeField] private int topScore = 0;


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

        public int CurrentScore { get => currentScore; set => currentScore = value; }
        public int TopScore { get => topScore; set => topScore = value; }

        Game()
        {
            Instance = this;
        }

        void Start()
        {
            HideAllUI();
            SetMenu();
        }

        void Update() => state.Update();
        void FixedUpdate() => state.FixedUpdate();

        public void SetMenu() => State = new MenuState(menu);
        public void SetPlaying() => State = new PlayingState(spawner, hud);
        public void SetGameOver() {
            Debug.Log("Game over");
        }

        public void InceaseScore(int amount)
        {
            currentScore += amount;
        }

        public void SpawnBall() 
        {
            // Spawn, hold then drop
            var go = Instantiate(ballPrefab.gameObject, parent.transform.position, Quaternion.identity, parent);
            currentBall = go.GetComponent<Ball>();
            var color = colors[Random.Range(0, colors.Count)];
            currentBall.SetColor(color);

        }

        private void HideAllUI()
        {
            menu.Hide();
            hud.Hide();
        }
    }
}