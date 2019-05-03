using Escapa.Managers;

namespace Escapa.Components
{
    public sealed class HighScoreLabel : Label
    {
        private new void Awake()
        {
            base.Awake();
            disableTranslating = true;
        }
        
        private new void Start()
        {
            base.Start();
            TextMesh.SetText(ScoreManager.CurrentTop.ToString("0.000"));
        }
    }
}