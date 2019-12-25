using Escapa.Utility;

namespace Escapa.Core.Interfaces
{
    public interface ILabel
    {
        LanguageTokens Token { get; set; }
        void SetText(string text);
    }
}
