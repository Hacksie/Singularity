using UnityEngine;

namespace HackedDesign.UI
{
    public class MenuPresenter : AbstractPresenter
    {
        [SerializeField] UnityEngine.UI.Text topScore;
        [SerializeField] UnityEngine.UI.Text currentScore;
        
        public override void Repaint()
        {
            topScore.text = Game.Instance.TopScore.ToString();
            currentScore.text = Game.Instance.CurrentScore.ToString();
        }

        public void OnPlayClick()
        {
            Game.Instance.SetPlaying();
        }

    }
}