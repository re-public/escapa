using Escapa.Core.Managers;
using Escapa.Utility;

namespace Escapa.UI
{
    public sealed class NewHighScoreLabel : Label
    {        
        private new void Start()
        {
            base.Start();
            gameObject.SetActive(ScoreManager.IsHighScore);
            TextMesh.SetText(LanguageManager.GetString(LanguageTokens.NewHighScore));
        }
    }
}