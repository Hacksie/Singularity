using UnityEngine;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private float nextSpawn = 0;
        public PlayingState()
        {
        }

        public bool Playing => false;

        public void Begin()
        {
            
        }

        public void End()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void Start()
        {
               
        }

        public void Update()
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + 5.0f;
                Game.Instance.SpawnBall(0);
            }
        }
    }
}