using UnityEngine;

namespace HackedDesign.UI
{
    public class HudPresenter : AbstractPresenter
    {
        [SerializeField] UnityEngine.UI.Text currentScore;
        [SerializeField] UnityEngine.UI.Text topScore;
        [SerializeField] UnityEngine.UI.Text time;
        public override void Repaint()
        {
            currentScore.text = Game.Instance.CurrentScore.ToString();
            topScore.text = Game.Instance.TopScore.ToString();
            time.text = System.TimeSpan.FromSeconds(Time.time).ToString("mm':'ss");
            //countdownText.text = System.TimeSpan.FromMinutes(Game.Instance.Data.Time).ToString("hh':'mm");
            //willSlider.value = Game.Instance.Data.Will;
        }
    }
}