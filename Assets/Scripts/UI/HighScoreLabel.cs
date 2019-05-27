using Escapa.Core.Managers;

namespace Escapa.UI
{
    public sealed class HighScoreLabel : Label
    {
        private new void Start()
        {
            base.Start();
            TextMesh.SetText(ScoreManager.CurrentHigh.ToString("0.000"));
        }
    }
}