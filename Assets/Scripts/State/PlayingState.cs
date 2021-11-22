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
            this.hud.Show();
            Time.timeScale = 1;
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
            if(spawner.CurrentBall == null || spawner.CurrentBall.dropped && spawner.CurrentBall.IsStopped())
            {
                spawner.DropNextBall();
            }
            this.hud.Repaint();
            /*
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + 5.0f;
                spawner.DropBall();
            }*/
        }
    }
}