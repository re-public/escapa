using Escapa.Core.Managers;
using Escapa.Utility;

namespace Escapa.UI
{
    public sealed class TimeLabel : Label
    {
        private new void Start()
        {
            base.Start();
            TextMesh.SetText(LanguageManager.GetString(LanguageTokens.Time) + ScoreManager.LastTime.ToString("0.000"));
        }
    }
}