using UnityEngine;

namespace HackedDesign
{
    public class MenuState : IState
    {
        private float nextSpawn = 0;
        private UI.AbstractPresenter menu;
        public MenuState(UI.AbstractPresenter menu)
        {
            this.menu = menu;
        }

        public bool Playing => false;

        public void Begin()
        {
            Time.timeScale = 0;
            this.menu.Show();
            this.menu.Repaint();   
        }

        public void End()
        {
            this.menu.Hide();   
        }

        public void FixedUpdate()
        {
            
        }

        public void Start()
        {
               
        }

        public void Update()
        {
           
        }
    }
}