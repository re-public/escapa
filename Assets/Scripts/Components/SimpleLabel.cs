using Escapa.Core.Managers;
using Escapa.Utility;

namespace Escapa.Components
{
    public sealed class SimpleLabel : Label
    {
        public LanguageTokens token;

        private new void Start()
        {
            base.Start();
            TextMesh.SetText(LanguageManager.GetString(token));
        }
    }
}