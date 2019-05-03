using Escapa.Managers;
using Escapa.Utility;

namespace Escapa.Components
{
    public sealed class TimeCounterLabel : Label
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
            TextMesh.SetText(string.Empty);
        }

        private void FixedUpdate()
        {
            TextMesh.SetText(ScoreManager.CurrentRecord.ToString("0.00"));
        }
    }
}