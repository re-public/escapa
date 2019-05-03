using Escapa.Managers;
using Escapa.Utility;

namespace Escapa.Components
{
    public sealed class TimeLabel : Label
    {
        private new void Awake()
        {
            base.Awake();
            disableTranslating = true;
            token = LanguageTokens.Time;
        }
        
        private new void Start()
        {
            base.Start();
            TextMesh.SetText(LanguageManager.GetString(token) + ScoreManager.LastTime.ToString("0.000"));
        }
    }
}