using UnityEngine;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private float nextSpawn = 0;
        private Spawner spawner;
        private UI.AbstractPresenter hud;
        public PlayingState(Spawner spawner, UI.AbstractPresenter hud)
        {
            this.spawner = spawner;
            this.hud = hud;
        }

        public bool Playing => true;

        public void Begin()
        {
            Reset();
            this.hud.Show();
        }

        public void End()
        {
            this.hud.Hide();
        }

        public void FixedUpdate()
        {
            
        }

        public void Start()
        {
               
        }

        public void Update()
        {
            if(Game.Instance.CurrentScore > Game.Instance.TopScore)
            {
                Game.Instance.TopScore = Game.Instance.CurrentScore;
            }

            if(spawner.CurrentBall == null || spawner.CurrentBall.dropped && spawner.CurrentBall.IsStopped())
            {
                Debug.Log("Drop");
                spawner.DropNextBall();
            }
            this.hud.Repaint();
        }

        private void Reset()
        {
            Time.timeScale = 1;
            Game.Instance.CurrentScore = 0;
            Physics2D.gravity = new Vector2(0, -0.9f);
            this.spawner.Reset();
            this.spawner.SpawnBall();
        }
    }
}