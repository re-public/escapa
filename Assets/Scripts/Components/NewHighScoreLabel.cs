using Escapa.Managers;
using Escapa.Utility;

namespace Escapa.Components
{
    public sealed class NewHighScoreLabel : Label
    {
        private new void Awake()
        {
            base.Awake();
            token = LanguageTokens.NewHighScore;
        }
        
        private new void Start()
        {
            base.Start();
            gameObject.SetActive(ScoreManager.IsHighScore);
        }
    }
}